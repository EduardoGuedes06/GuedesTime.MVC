using AutoMapper;
using GuedesTime.Domain.Intefaces;
using GuedesTime.Domain.Models;
using GuedesTime.Domain.Models.Enums;
using GuedesTime.MVC.Extensions;
using GuedesTime.MVC.Models;
using GuedesTime.MVC.ViewModels;
using GuedesTime.MVC.ViewModels.Enum;
using GuedesTime.MVC.ViewModels.Utils;
using GuedesTime.MVC.ViewModels.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace GuedesTime.MVC.Controllers
{
	[Authorize]
	public class SerieController : BaseController
	{
		private readonly IMapper _mapper;
		private readonly IInstituicaoService _instituicaoService;
		private readonly ISerieService _serieService;
		private readonly UserManager<ApplicationUser> _userManager;

		public SerieController(IMapper mapper,
									INotificador notificador,
									IInstituicaoService instituicaoService,
									UserManager<ApplicationUser> userManager,
									ISerieService serieService) : base(notificador)
		{
			_mapper = mapper;
			_userManager = userManager;
			_instituicaoService = instituicaoService;
			_serieService = serieService;
		}

		public async Task<IActionResult> Index(
			string? search,
			EnumTipoEnsino? tipoEnsino,
			bool? ativo = true,
			int? page = 1,
			int? pageSize = ((int)EnumQuantidadeDeItensPorPagina.Muitos))
		{
			var userId = Guid.Parse(_userManager.GetUserId(User));
			var instituicaoId = Guid.Parse(HttpContext.Session.GetString("InstituicaoId"));

			if (!await _instituicaoService.VerificaUsuarioInstituicao(userId, instituicaoId))
				return NotFound();

			var ensinoItems = EnumTipoEnsinoViewModel.Ensino_Infantil.ToSelectListItems();
			ViewBag.TipoEnsinoOptions = ensinoItems;

			Expression<Func<Serie, bool>>? filtroAdicional = null;
			if (tipoEnsino.HasValue)
			{
				var tipoEnsinoFiltro = tipoEnsino.Value;
				filtroAdicional = s => s.TipoEnsino == tipoEnsinoFiltro;
			}

			var pagedSerie = await _serieService.GetPagedByInstituicaoAsync(
				instituicaoId,
				search,
				page.Value,
				pageSize.Value,
				ativo.Value,
				filtroAdicional: filtroAdicional,
				ordenacao: q => q.OrderBy(s => s.TipoEnsino).ThenBy(s => s.Nome),
				includes: s => s.Disciplinas
			);

			var serieViewModels = _mapper.Map<IEnumerable<SerieViewModel>>(pagedSerie.Items);

			var pagedViewModel = new PagedViewModel<SerieViewModel>
			{
				Model = serieViewModels,
				Search = search,
				Page = page.Value,
				PageSize = pageSize.Value,
				TotalPages = (int)Math.Ceiling((double)pagedSerie.TotalCount / pageSize.Value),
				TotalItems = pagedSerie.TotalCount
			};

			ViewBag.FiltroAtivo = ativo;
			ViewBag.FiltroTipoEnsino = tipoEnsino;

			if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
			{
				return PartialView("_SerieListPartial", pagedViewModel);
			}
			return View(pagedViewModel);
		}



		[HttpGet]
		public async Task<IActionResult> Upsert(Guid? id)
		{
			var serieViewModel = new SerieViewModel();

			var userId = Guid.Parse(_userManager.GetUserId(User));
			var instituicaoId = Guid.Parse(HttpContext.Session.GetString("InstituicaoId"));
			if (id.HasValue)
			{
				var serie = await _serieService.ObterPorId(id.Value);
				if (serie == null) return NotFound();

				serieViewModel = _mapper.Map<SerieViewModel>(serie);
				serieViewModel.SerieUnica = serieViewModel.Nome;
				serieViewModel.InstituicaoId = instituicaoId;

			}
			else
			{
				serieViewModel = new SerieViewModel
				{
					InstituicaoId = instituicaoId
				};
			}

			ViewBag.EstadoInicialAtivo = serieViewModel.Ativo.GetValueOrDefault(true);
			serieViewModel.ListaTipoEnsino = EnumTipoEnsinoViewModel.Todos.ToSelectListItemsFiltered();

			return PartialView("_Upsert", serieViewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Upsert(SerieViewModel serieViewModel)
		{
			var validator = new SerieViewModelValidation();
			var validationResult = await validator.ValidateAsync(serieViewModel);

			if (!validationResult.IsValid)
			{
				var erros = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();
				return Json(new { success = false, errors = erros });
			}

			var instituicaoId = Guid.Parse(HttpContext.Session.GetString("InstituicaoId"));
			serieViewModel.InstituicaoId = instituicaoId;

			bool isCadastro = serieViewModel.Id == Guid.Empty || serieViewModel.Id == null;
			var tipoEnsinoDomain = _mapper.Map<EnumTipoEnsino>(serieViewModel.TipoEnsino);

			string? serieUnicaParaVerificar = !string.IsNullOrWhiteSpace(serieViewModel.SerieUnica) ? serieViewModel.SerieUnica : null;
			string? seriesMultiplasParaVerificar = !string.IsNullOrWhiteSpace(serieViewModel.SeriesMultiplas) ? serieViewModel.SeriesMultiplas : null;

			var nomesDuplicados = await _serieService.VerificarSeriesDuplicadasAsync(
				instituicaoId,
				serieUnicaParaVerificar,
				seriesMultiplasParaVerificar,
				tipoEnsinoDomain,
				isCadastro ? null : serieViewModel.Id);

			if (nomesDuplicados.Any())
			{
				var mensagensErro = nomesDuplicados
					.Select(n => $"A série '{n}' já existe para o tipo de ensino selecionado.")
					.ToArray();
				return Json(new { success = false, errors = mensagensErro });
			}

			if (isCadastro)
				return await ProcessarCadastroAsync(serieViewModel, instituicaoId);
			else
				return await ProcessarAtualizacaoAsync(serieViewModel);
		}



		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Desativar(Guid instituicaoId, Guid id)
		{
			var userId = Guid.Parse(_userManager.GetUserId(User));
			if (!await _instituicaoService.VerificaUsuarioInstituicao(userId, instituicaoId))
			{
				return NotFound(new { success = false, message = "Permissão negada ou instituição não encontrada." });
			}

			var serieExistente = await _serieService.ObterPorId(id);
			if (serieExistente == null)
			{
				return NotFound(new { success = false, message = "Série não encontrada!" });
			}

			try
			{
				if (serieExistente.Ativo.Value)
				{
					serieExistente.Ativo = false;
					await _serieService.Atualizar(serieExistente);
					return Json(new { success = true, message = "Série desativada com sucesso!" });
				}
				else
				{
					await _serieService.Remover(id);
					return Json(new { success = true, message = "Série removida permanentemente com sucesso!" });
				}
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { success = false, message = "Ocorreu um erro interno ao processar sua solicitação." });
			}
		}

		#region crud suport

		private string? ValidarCampos(bool isCadastro, bool preencheuSerieUnica, bool preencheuSeriesMultiplas)
		{
			if (isCadastro && !preencheuSeriesMultiplas)
				return "Informe ao menos uma série no campo 'Registro de múltiplas Séries'.";

			if (!isCadastro && !preencheuSerieUnica)
				return "Informe uma série no campo 'Série'.";

			if (preencheuSerieUnica && preencheuSeriesMultiplas)
				return "Preencha apenas um dos campos: 'Série' ou 'Registro de múltiplas Séries'.";

			return null;
		}

		private async Task<IActionResult> ProcessarCadastroAsync(SerieViewModel serieViewModel, Guid instituicaoId)
		{
			var nomesSeries = serieViewModel.SeriesMultiplas?
				.Split(',', StringSplitOptions.RemoveEmptyEntries)
				.Select(n => n.Trim())
				.Where(n => !string.IsNullOrEmpty(n))
				.ToList();

			if (nomesSeries == null || !nomesSeries.Any())
				return Json(new { success = false, errors = new[] { "Nenhuma série válida informada." } });

			var viewModels = nomesSeries.Select(nome => new SerieViewModel
			{
				Nome = nome,
				InstituicaoId = instituicaoId,
				TipoEnsino = serieViewModel.TipoEnsino,
				Ativo = serieViewModel.Ativo ?? true,
				Codigo = null,
				DataCriacao = DateTime.UtcNow
			}).ToList();

			var entidades = _mapper.Map<List<Serie>>(viewModels);

			await _serieService.AdicionarVariasAsync(entidades);

			TempData["success"] = "Séries cadastradas com sucesso!";
			return Json(new { success = true, url = Url.Action("index", "Serie") });
		}

		private async Task<IActionResult> ProcessarAtualizacaoAsync(SerieViewModel serieViewModel)
		{
			var serieExiste = await _serieService.ObterPorId((Guid)serieViewModel.Id);
			if (serieExiste == null)
				return Json(new { success = false, errors = new[] { "Série não encontrada." } });

			serieViewModel.Nome = serieViewModel.SerieUnica;
			serieViewModel.Codigo = serieExiste.Codigo ?? 0;

			_mapper.Map(serieViewModel, serieExiste);
			await _serieService.Atualizar(serieExiste);

			TempData["success"] = "Dados da Série alterados com sucesso!";

			if (!OperacaoValida())
				return Json(new { success = false, errors = ObterErrosDeNegocio() });

			return Json(new { success = true, url = Url.Action("index", "Serie") });
		}

		#endregion

	}
}
