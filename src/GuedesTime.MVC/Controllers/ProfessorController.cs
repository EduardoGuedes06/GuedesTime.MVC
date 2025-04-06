using AutoMapper;
using GuedesTime.Domain.Intefaces;
using GuedesTime.Domain.Models;
using GuedesTime.MVC.ViewModels;
using GuedesTime.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GuedesTime.MVC.Controllers
{
    [Authorize]
    public class ProfessorController : BaseController
    {
        private readonly IMapper _mapper;

        public ProfessorController(IMapper mapper, INotificador notificador) : base(notificador)
        {
            _mapper = mapper;
        }


        // GET: ProfessorController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ProfessorController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //public async Task<IActionResult> Upsert(Guid? id)
        //{
        //    ProfessorViewModel professorViewModel = new();

        //    if (id.HasValue)
        //    {
        //        var instituicao = await _instituicaoService.ObterInstituicaoComEnderecoPorId(id.Value);
        //        if (instituicao == null) return NotFound();
        //        professorViewModel = _mapper.Map<InstituicaoViewModel>(instituicao);


        //    }
        //    else { professorViewModel.UsuarioId = null; }

        //    return View(instituicaoViewModel);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Upsert(InstituicaoViewModel instituicaoViewModel)
        //{
        //    ModelState.Remove(nameof(instituicaoViewModel.UsuarioId));
        //    instituicaoViewModel.UsuarioId = Guid.Parse(_userManager.GetUserId(User));

        //    if (!ModelState.IsValid) return View(instituicaoViewModel);


        //    if (instituicaoViewModel.Id == Guid.Empty || instituicaoViewModel.Id == null)
        //    {
        //        instituicaoViewModel.Avatar = await _instituicaoService.ObterAvatarAleatorioAsync();
        //        await _instituicaoService.Adicionar(_mapper.Map<Instituicao>(instituicaoViewModel));
        //    }
        //    else
        //    {
        //        var instituicaoExistente = await _instituicaoService.ObterPorId((Guid)instituicaoViewModel.Id);
        //        if (instituicaoExistente == null) return NotFound();
        //        instituicaoViewModel.Avatar = instituicaoExistente.Avatar;


        //        _mapper.Map(instituicaoViewModel, instituicaoExistente);
        //        await _instituicaoService.Atualizar(instituicaoExistente);
        //    }

        //    return OperacaoValida() ? RedirectToAction(nameof(Index)) : View(instituicaoViewModel);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid id, InstituicaoViewModel instituicaoViewModel)
        //{
        //    if (id != instituicaoViewModel.Id) return BadRequest();
        //    if (!ModelState.IsValid) return View(instituicaoViewModel);

        //    await _instituicaoService.Atualizar(_mapper.Map<Instituicao>(instituicaoViewModel));

        //    if (!OperacaoValida()) return View(instituicaoViewModel);

        //    return RedirectToAction(nameof(Index));
        //}

        // GET: ProfessorController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProfessorController/Delete/5
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
