using AutoMapper;
using GuedesTime.Domain.Intefaces;
using GuedesTime.Domain.Models;
using GuedesTime.MVC.Models;
using GuedesTime.MVC.ViewModels;
using GuedesTime.MVC.ViewModels.Utils;
using GuedesTime.Service.Services;
using k8s.KubeConfigModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GuedesTime.MVC.Controllers
{
    [Authorize]
    public class DisciplinaController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IInstituicaoService _instituicaoService;
        private readonly IDisciplinaService _disciplinaService;
        private readonly UserManager<ApplicationUser> _userManager;

        public DisciplinaController(IMapper mapper,
                                    INotificador notificador,
                                    IInstituicaoService instituicaoService,
                                    UserManager<ApplicationUser> userManager,
                                    IDisciplinaService idisciplinaService) : base(notificador)
        {
            _mapper = mapper;
            _userManager = userManager;
            _instituicaoService = instituicaoService;
            _disciplinaService = idisciplinaService;
        }

		public ActionResult Index()
		{
			return View();
		}


		public async Task<IActionResult> ConsultaDiciplinas(Guid id, string? search, int page = 1, int pageSize = 5, bool ativo = true)
        {

            var userId = Guid.Parse(_userManager.GetUserId(User));

            if (!await _instituicaoService.VerificaUsuarioInstituicao(userId, id))
                return NotFound();

            ViewBag.InstituicaoId = id;
            var pagedDisciplinas = await _disciplinaService.GetPagedByInstituicaoAsync(id, search, page, pageSize, ativo);

            var disciplinaViewModels = _mapper.Map<IEnumerable<DisciplinaViewModel>>(pagedDisciplinas.Items);

            var pagedViewModel = new PagedViewModel<DisciplinaViewModel>
            {
                Model = disciplinaViewModels,
                Search = search,
                Page = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)pagedDisciplinas.TotalCount / pageSize),
                TotalItems = pagedDisciplinas.TotalCount
            };
            return View(pagedViewModel);
        }

        public async Task<IActionResult> UpsertPartial(Guid instituicaoId, Guid? id)
        {
            var userId = Guid.Parse(_userManager.GetUserId(User));

            if (!await _instituicaoService.VerificaUsuarioInstituicao(userId, instituicaoId))
                return NotFound();

            DisciplinaViewModel disciplinaViewModel;

            if (id.HasValue)
            {
                var disciplina = await _disciplinaService.ObterPorId(id.Value);
                if (disciplina == null) return NotFound();

                disciplinaViewModel = _mapper.Map<DisciplinaViewModel>(disciplina);
            }
            else
            {
                disciplinaViewModel = new DisciplinaViewModel
                {
                    InstituicaoId = instituicaoId
                };
            }

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_UpsertPartial", disciplinaViewModel);
            }

            return View(disciplinaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
		public async Task<IActionResult> Upsert(DisciplinaViewModel disciplinaViewModel)
		{
			var referer = Request.Headers["Referer"].ToString();
			var userId = Guid.Parse(_userManager.GetUserId(User));

			if (!await UsuarioTemAcessoInstituicao(userId, disciplinaViewModel.InstituicaoId))
				return NotFound();

			if (!ModelState.IsValid)
				return TratarModelStateInvalido(referer);

			if (disciplinaViewModel.Id == null || disciplinaViewModel.Id == Guid.Empty)
			{
				if (await TentarAdicionarDisciplinasMultiplas(disciplinaViewModel, referer))
					return Redirect(referer);
				if (await ExisteDisciplinaComMesmoNome(disciplinaViewModel))
				{
					TempData["error"] = "Essa Disciplina já existe!";
					return Redirect(referer);
				}

				await CriarNovaDisciplina(disciplinaViewModel);
			}
			else
			{
				if (await ExisteDisciplinaComMesmoNome(disciplinaViewModel))
				{
					TempData["error"] = "Essa Disciplina já existe!";
					return Redirect(referer);
				}

				await AtualizarDisciplinaExistente(disciplinaViewModel);
			}

			if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
				return Json(new { success = true });

			if (!string.IsNullOrEmpty(referer))
				return Redirect(referer);

			return RedirectToAction("Detalhes", "Instituicao", new { id = disciplinaViewModel.InstituicaoId });
		}


		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid instituicaoId, Guid id)
        {
            var userId = Guid.Parse(_userManager.GetUserId(User));
            if (!await _instituicaoService.VerificaUsuarioInstituicao(userId, instituicaoId))
                return NotFound();

            var client = await _disciplinaService.ObterPorId(id);
            if (client == null)
            {
                TempData["warning"] = "Disciplina não encontrada!!";
                return NotFound();
            }

            var disciplinaExistente = await _disciplinaService.ObterPorId((Guid)id);
            if (disciplinaExistente == null) return NotFound();

            if (disciplinaExistente.Ativo == true)
            { disciplinaExistente.Ativo = false; await _disciplinaService.Atualizar(disciplinaExistente); TempData["success"] = "Disciplina desativada com sucesso!!"; }
            else { await _disciplinaService.Remover(id); TempData["success"] = "Disciplina Deletada do sistema com sucesso!!"; }

            var referer = Request.Headers["Referer"].ToString();
            if (!string.IsNullOrEmpty(referer))
            {
                return Redirect(referer);
            }
            return Ok();
        }

		#region Funções de Apoio

		private async Task<bool> UsuarioTemAcessoInstituicao(Guid userId, Guid instituicaoId)
		{
			return await _instituicaoService.VerificaUsuarioInstituicao(userId, instituicaoId);
		}

		private async Task<bool> TentarAdicionarDisciplinasMultiplas(DisciplinaViewModel vm, string referer)
		{
			var nomesMultiplos = Request.Form["Nomes"].ToString();

			if (!string.IsNullOrWhiteSpace(vm.Nome) || string.IsNullOrWhiteSpace(nomesMultiplos))
				return false;

			if (!ValidarNomesMultiplos(nomesMultiplos))
				return false;

			var listaNomes = nomesMultiplos
				.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
				.ToList();

			var (existe, nomesExistentes) = await _disciplinaService.VerificarDisciplinasExistentesPorNomes(vm.InstituicaoId, listaNomes);

			if (existe)
			{
				TempData["error"] = $"As seguintes disciplinas já existem: {string.Join(", ", nomesExistentes)}";
				return true;
			}

			try
			{
				await _disciplinaService.AdicionarDisciplinas(vm.InstituicaoId, listaNomes);
				TempData["success"] = "Disciplinas adicionadas com sucesso!";
				return true;
			}
			catch (Exception ex)
			{
				TempData["error"] = $"Erro ao adicionar disciplinas: {ex.Message}";
				return true;
			}
		}

		private IActionResult TratarModelStateInvalido(string referer)
		{
			if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
			{
				return Json(new
				{
					success = false,
					errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
				});
			}

			TempData["error"] = "Estado da página inválido! Recarregue a página!";
			if (!string.IsNullOrEmpty(referer))
				return Redirect(referer);

			return Ok();
		}

		private async Task<bool> ExisteDisciplinaComMesmoNome(DisciplinaViewModel disciplinaViewModel)
		{
			var disciplinaComMesmoNome = await _disciplinaService.ObterDisciplinaPorNome(disciplinaViewModel.InstituicaoId, disciplinaViewModel.Nome);
			return disciplinaComMesmoNome != null &&
				   (disciplinaViewModel.Id == null || disciplinaComMesmoNome.Id != disciplinaViewModel.Id) &&
				   disciplinaComMesmoNome.Ativo == disciplinaViewModel.Ativo;
		}

		private async Task CriarNovaDisciplina(DisciplinaViewModel vm)
		{
			var novaDisciplina = _mapper.Map<Disciplina>(vm);
			await _disciplinaService.Adicionar(novaDisciplina);
			TempData["success"] = "Disciplina cadastrada com sucesso!";
		}

		private async Task AtualizarDisciplinaExistente(DisciplinaViewModel vm)
		{
			var disciplinaExistente = await _disciplinaService.ObterPorId(vm.Id.Value);
			if (disciplinaExistente == null)
				throw new Exception("Disciplina não encontrada.");

			_mapper.Map(vm, disciplinaExistente);
			await _disciplinaService.Atualizar(disciplinaExistente);
			TempData["success"] = "Dados da disciplina alterados com sucesso!";
		}

		#endregion
	}
}
