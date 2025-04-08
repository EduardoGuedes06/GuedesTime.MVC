using AutoMapper;
using GuedesTime.Domain.Intefaces;
using GuedesTime.Domain.Models;
using GuedesTime.MVC.Models;
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
        private readonly IProfessorService _professorService;
        private readonly IInstituicaoService _instituicaoService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfessorController(IMapper mapper, INotificador notificador,
                                    UserManager<ApplicationUser> userManager,
                                    IProfessorService professorService,
                                    IInstituicaoService instituicaoService) : base(notificador)
        {
            _mapper = mapper;
            _professorService = professorService;
            _userManager = userManager;
            _instituicaoService = instituicaoService;
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

        public async Task<IActionResult> Upsert(Guid InstituicaoId, Guid? id)
        {
            ProfessorViewModel professorViewModel = new();
            var UserId = Guid.Parse(_userManager.GetUserId(User));

            if (await _instituicaoService.VerificaUsuarioInstituicao(UserId, InstituicaoId))
            {
                if (id.HasValue)
                {
                    var professor = await _professorService.ObterPorId(id.Value);
                    if (professor == null) return NotFound();
                    professorViewModel = _mapper.Map<ProfessorViewModel>(professor);


                }
                else { professorViewModel = null; }

                return View(professorViewModel);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ProfessorViewModel professorViewModel)
        {
            //ModelState.Remove(nameof(instituicaoViewModel.UsuarioId));
            //instituicaoViewModel.UsuarioId = Guid.Parse(_userManager.GetUserId(User));

            //if (!ModelState.IsValid) return View(instituicaoViewModel);


            //if (instituicaoViewModel.Id == Guid.Empty || instituicaoViewModel.Id == null)
            //{
            //    instituicaoViewModel.Avatar = await _instituicaoService.ObterAvatarAleatorioAsync();
            //    await _instituicaoService.Adicionar(_mapper.Map<Instituicao>(instituicaoViewModel));
            //}
            //else
            //{
            //    var instituicaoExistente = await _instituicaoService.ObterPorId((Guid)instituicaoViewModel.Id);
            //    if (instituicaoExistente == null) return NotFound();
            //    instituicaoViewModel.Avatar = instituicaoExistente.Avatar;


            //    _mapper.Map(instituicaoViewModel, instituicaoExistente);
            //    await _instituicaoService.Atualizar(instituicaoExistente);
            //}

            return OperacaoValida() ? RedirectToAction(nameof(Index)) : View(professorViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProfessorViewModel professorViewModel)
        {
            //if (id != instituicaoViewModel.Id) return BadRequest();
            //if (!ModelState.IsValid) return View(instituicaoViewModel);

            //await _instituicaoService.Atualizar(_mapper.Map<Instituicao>(instituicaoViewModel));

            //if (!OperacaoValida()) return View(instituicaoViewModel);

            return RedirectToAction(nameof(Index));
        }

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
