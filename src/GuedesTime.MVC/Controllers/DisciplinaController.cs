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


        // GET: DisciplinaController
        public async Task<IActionResult> Index(Guid id, string? search, int page = 1, int pageSize = 5, bool ativo = true)
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

            if (!await _instituicaoService.VerificaUsuarioInstituicao(userId, disciplinaViewModel.InstituicaoId))
                return NotFound();

            if (!ModelState.IsValid)
            {
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new
                    {
                        success = false,
                        errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                    });
                }

                return View(disciplinaViewModel);
            }

            var disciplinaComMesmoNome = await _disciplinaService.ObterDisciplinaPorNome(
                disciplinaViewModel.InstituicaoId,
                disciplinaViewModel.Nome
            );

            if (disciplinaComMesmoNome != null &&
                (disciplinaViewModel.Id == null || disciplinaComMesmoNome.Id != disciplinaViewModel.Id) &&
                disciplinaComMesmoNome.Ativo == disciplinaViewModel.Ativo)
            {
                TempData["error"] = "Já existe uma disciplina com esse nome!";
                return Redirect(referer);
            }

            if (disciplinaViewModel.Id == null || disciplinaViewModel.Id == Guid.Empty)
            {
                var novaDisciplina = _mapper.Map<Disciplina>(disciplinaViewModel);
                await _disciplinaService.Adicionar(novaDisciplina);
                TempData["success"] = "Disciplina cadastrada com sucesso!";
            }
            else
            {
                var disciplinaExistente = await _disciplinaService.ObterPorId(disciplinaViewModel.Id.Value);
                if (disciplinaExistente == null)
                    return NotFound();

                _mapper.Map(disciplinaViewModel, disciplinaExistente);
                await _disciplinaService.Atualizar(disciplinaExistente);
                TempData["success"] = "Dados da disciplina alterados com sucesso!";
            }

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return Json(new { success = true });
            }

            if (!string.IsNullOrEmpty(referer))
            {
                return Redirect(referer);
            }

            return RedirectToAction("Detalhes", "Instituicao", new { id = disciplinaViewModel.InstituicaoId });
        }




        // GET: DisciplinaController/Delete/5
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
    }
}
