using AutoMapper;
using GuedesTime.Domain.Intefaces;
using GuedesTime.MVC.Models;
using GuedesTime.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GuedesTime.MVC.Controllers
{
    [Authorize]
    public class DashboardController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IInstituicaoService _instituicaoService;
        private readonly UserManager<ApplicationUser> _userManager;

        public DashboardController(IMapper mapper, INotificador notificador, IInstituicaoService instituicaoService, UserManager<ApplicationUser> userManager) : base(notificador)
        {
            _mapper = mapper;
            _instituicaoService = instituicaoService;
            _userManager = userManager;
        }


        // GET: DashboardController
        public async Task<IActionResult> Index()
        {
            var usuarioId = Guid.Parse(_userManager.GetUserId(User));
            var instituicoes = await _instituicaoService.ObterDadosInstituicoesUsuario(usuarioId);

            var instituicoesViewModel = _mapper.Map<IEnumerable<InstituicaoViewModel>>(instituicoes);

            if (!instituicoes.Any())
            {
                return RedirectToAction("Upsert", "Instituicao");
            }

            var viewModel = new DashboardViewModel
            {
                Instituicoes = instituicoesViewModel
            };

            return View(viewModel);
        }

        // GET: LogController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LogController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LogController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: LogController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LogController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: LogController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LogController/Delete/5
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
