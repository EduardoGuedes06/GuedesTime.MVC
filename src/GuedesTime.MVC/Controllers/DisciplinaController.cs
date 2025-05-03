using AutoMapper;
using GuedesTime.Domain.Intefaces;
using GuedesTime.Domain.Models;
using GuedesTime.MVC.Models;
using GuedesTime.MVC.ViewModels;
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
        public ActionResult Index()
        {
            return View();
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
            var userId = Guid.Parse(_userManager.GetUserId(User));
            if (!await _instituicaoService.VerificaUsuarioInstituicao(userId, disciplinaViewModel.InstituicaoId))
                return NotFound();

            if (!ModelState.IsValid)
            {
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });
                }
                return View(disciplinaViewModel);
            }

            if (disciplinaViewModel.Id == Guid.Empty || disciplinaViewModel.Id == null)
            {
                await _disciplinaService.Adicionar(_mapper.Map<Disciplina>(disciplinaViewModel));
            }
            else
            {
                var disciplinaExistente = await _disciplinaService.ObterPorId((Guid)disciplinaViewModel.Id);
                if (disciplinaExistente == null) return NotFound();

                _mapper.Map(disciplinaViewModel, disciplinaExistente);
                await _disciplinaService.Atualizar(disciplinaExistente);
            }

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return Json(new { success = true });
            }
            return RedirectToAction(nameof(Upsert), new { instituicaoId = disciplinaViewModel.InstituicaoId });
        }



        // GET: DisciplinaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DisciplinaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
