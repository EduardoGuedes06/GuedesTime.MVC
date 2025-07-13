using AutoMapper;
using GuedesTime.Domain.Intefaces;
using GuedesTime.Domain.Models;
using GuedesTime.MVC.Models;
using GuedesTime.MVC.ViewModels;
using GuedesTime.MVC.ViewModels.Enum;
using GuedesTime.MVC.ViewModels.Utils;
using GuedesTime.Service.Services;
using k8s.KubeConfigModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace GuedesTime.MVC.Controllers
{
	[Authorize]
	public class InstituicaoController : BaseController
	{
		private readonly IMapper _mapper;
		private readonly IInstituicaoService _instituicaoService;
		private readonly UserManager<ApplicationUser> _userManager;


		public InstituicaoController(IMapper mapper,
									 IInstituicaoService instituicaoService,
									 UserManager<ApplicationUser> userManager,
									 INotificador notificador) : base(notificador)
		{
			_mapper = mapper;
			_instituicaoService = instituicaoService;
			_userManager = userManager;
		}

		public IActionResult Definir(Guid id)
		{
			if (id == Guid.Empty) return NotFound();

			HttpContext.Session.SetString("InstituicaoId", id.ToString());

			TempData["success"] = "Instituição selecionada com sucesso!";
			return RedirectToAction("Index", "Painel");
		}

		public IActionResult Index()
		{
			var idInstituicao = HttpContext.Session.GetString("InstituicaoId");

			return View();
		}

		public async Task<IActionResult> SelecionarInstituicao(string? search, int? page = 1, bool? ativo = true)
		{
			var UserId = Guid.Parse(_userManager.GetUserId(User));

			var instituicoes = await _instituicaoService.GetPagedByInstituicaoAsync(
																			UserId,
																			search,
																			page.Value,
																			(int)EnumQuantidadeDeItensPorPagina.Poucos,
																			ativo.Value,
																			filtroAdicional: null,
																			ordenacao: q => q.OrderBy(s => s.Nome),
																			includes: null
																		);

			var instituicaoIds = instituicoes.Items.Select(i => i.Id).ToList();
			var dadosResumo = _mapper.Map<Dictionary<Guid, DadosAgregadosInstituicaoViewModel>>(await _instituicaoService.ObterCalculoGeralDosDadosDaInstituicao(instituicaoIds));
			ViewBag.ResumoInstituicoes = dadosResumo;
			var instituicoesViewModel = _mapper.Map<IEnumerable<InstituicaoViewModel>>(instituicoes.Items);
			var paged = new PagedViewModel<InstituicaoViewModel>
			{
				Model = instituicoesViewModel,
				Search = search,
				Page = page.Value,
				PageSize = 5,
				TotalPages = (int)Math.Ceiling((double)instituicoes.TotalCount / 5),
				TotalItems = instituicoes.TotalCount
			};
			return View(paged);
		}

		[AllowAnonymous]
		public async Task<IActionResult> Detalhes(Guid id)
		{
			var instituicao = await _instituicaoService.ObterDadosInstituicoesPorId(id);
			var instituicoesViewModel = _mapper.Map<InstituicaoViewModel>(instituicao);

			var user = await _userManager.FindByIdAsync(instituicoesViewModel.UsuarioId.ToString());
			var userName = user?.UserName;
			ViewBag.Nome = userName;

			var progresso = VerificarPendencias(instituicoesViewModel);

			var totalEtapas = 8;
			var percentual = (progresso.EtapasConcluidas * 100) / totalEtapas;
			ViewBag.Progresso = percentual;
			ViewBag.EtapaAtual = progresso.EtapasConcluidas;
			ViewBag.ProximaEtapa = progresso.ProximaEtapa;

			if (progresso.Pendencias.Any())
			{
				TempData["warning"] = $"Cadastros Pendentes: {string.Join(", ", progresso.Pendencias)}";
			}

			if (instituicoesViewModel == null) return NotFound();

			return View(instituicoesViewModel);
		}



		[HttpGet]
		public async Task<IActionResult> Upsert(Guid? id)
		{
			var instituicaoViewModel = new InstituicaoViewModel();

			if (id.HasValue)
			{
				var instituicao = await _instituicaoService.ObterInstituicaoComEnderecoPorId(id.Value);
				if (instituicao == null) return NotFound();

				instituicaoViewModel = _mapper.Map<InstituicaoViewModel>(instituicao);
				if (instituicaoViewModel.Endereco?.Cep != null)
				{
					instituicaoViewModel.Endereco.Cep = instituicaoViewModel.Endereco.Cep.Replace("-", "");
				}

				ViewBag.EstadoInicialAtivo = instituicaoViewModel.Ativo.GetValueOrDefault(true);

			}

			return PartialView("_Upsert", instituicaoViewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Upsert(InstituicaoViewModel instituicaoViewModel)
		{
			ModelState.Remove(nameof(instituicaoViewModel.UsuarioId));
			instituicaoViewModel.UsuarioId = Guid.Parse(_userManager.GetUserId(User));

			bool? estadoDoToggleEnviado = instituicaoViewModel.Ativo;


			if (!ModelState.IsValid)
			{
				var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
				return Json(new { success = false, errors });
			}

			if (instituicaoViewModel.Id == Guid.Empty || instituicaoViewModel.Id == null)
			{
				instituicaoViewModel.Avatar = await _instituicaoService.ObterAvatarAleatorioAsync();
				var instituicao = _mapper.Map<Instituicao>(instituicaoViewModel);
				await _instituicaoService.Adicionar(instituicao);
				TempData["success"] = "Instituição Cadastrada com sucesso!!";
			}
			else
			{
				var instituicaoExistente = await _instituicaoService.ObterPorId((Guid)instituicaoViewModel.Id);
				if (instituicaoExistente == null)
				{
					return Json(new { success = false, errors = new[] { "Instituição não encontrada." } });
				}
				instituicaoViewModel.Avatar = instituicaoExistente.Avatar;
				_mapper.Map(instituicaoViewModel, instituicaoExistente);
				await _instituicaoService.Atualizar(instituicaoExistente);
				TempData["success"] = "Dados da Instituição alterados com sucesso!!";
			}

			if (!OperacaoValida())
			{
				return Json(new { success = false, errors = ObterErrosDeNegocio() });
			}

			return Json(new { success = true, url = Url.Action("SelecionarInstituicao", "Instituicao") });
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Guid id, InstituicaoViewModel instituicaoViewModel)
		{
			if (id != instituicaoViewModel.Id) return BadRequest();
			if (!ModelState.IsValid) return View(instituicaoViewModel);

			await _instituicaoService.Atualizar(_mapper.Map<Instituicao>(instituicaoViewModel));

			if (!OperacaoValida()) return View(instituicaoViewModel);

			return RedirectToAction(nameof(Index));
		}

		private ProgressoCadastroViewModel VerificarPendencias(InstituicaoViewModel instituicoesViewModel)
		{
			var pendencias = new List<string>();
			var totalEtapas = 8;
			string proximaEtapa = null;

			if (instituicoesViewModel.Disciplinas == null || !instituicoesViewModel.Disciplinas.Any())
			{
				pendencias.Add("Disciplinas");
				if (proximaEtapa == null) proximaEtapa = "Disciplinas";
			}
			if (instituicoesViewModel.Series == null || !instituicoesViewModel.Series.Any())
			{
				pendencias.Add("Séries");
				if (proximaEtapa == null) proximaEtapa = "Séries";
			}
			if (instituicoesViewModel.Turmas == null || !instituicoesViewModel.Turmas.Any())
			{
				pendencias.Add("Turmas");
				if (proximaEtapa == null) proximaEtapa = "Turmas";
			}
			if (instituicoesViewModel.Professores == null || !instituicoesViewModel.Professores.Any())
			{
				pendencias.Add("Professores");
				if (proximaEtapa == null) proximaEtapa = "Professores";
			}
			if (instituicoesViewModel.Salas == null || !instituicoesViewModel.Salas.Any())
			{
				pendencias.Add("Salas");
				if (proximaEtapa == null) proximaEtapa = "Salas";
			}
			if (instituicoesViewModel.Feriados == null || !instituicoesViewModel.Feriados.Any())
			{
				pendencias.Add("Feriados");
				if (proximaEtapa == null) proximaEtapa = "Feriados";
			}
			if (instituicoesViewModel.Horarios == null || !instituicoesViewModel.Horarios.Any())
			{
				pendencias.Add("Horários");
				if (proximaEtapa == null) proximaEtapa = "Horários";
			}

			var etapasConcluidas = totalEtapas - pendencias.Count;

			if (etapasConcluidas < 0)
				etapasConcluidas = 0;

			return new ProgressoCadastroViewModel
			{
				EtapasConcluidas = etapasConcluidas,
				Pendencias = pendencias,
				ProximaEtapa = proximaEtapa
			};
		}

		[HttpGet]
		public async Task<bool> ValidaUsuarioInstituicao()
		{
			var userId = Guid.Parse(_userManager.GetUserId(User));
			var instituicaoId = Guid.Parse(HttpContext.Session.GetString("InstituicaoId"));
			if (!await _instituicaoService.VerificaUsuarioInstituicao(userId, instituicaoId))
			{
				return false;
			}

			return true;
		}

	}
}
