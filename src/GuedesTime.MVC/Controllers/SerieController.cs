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

        public async Task<IActionResult> Index(Guid id, string? search, int page = 1, int pageSize = 5, bool ativo = true)
        {
            var userId = Guid.Parse(_userManager.GetUserId(User));

            if (!await _instituicaoService.VerificaUsuarioInstituicao(userId, id))
                return NotFound();

            ViewBag.InstituicaoId = id;
            var pagedSerie = await _serieService.GetPagedByInstituicaoAsync(id, search, page, pageSize, ativo);

            var serieViewModels = _mapper.Map<IEnumerable<SerieViewModel>>(pagedSerie.Items);

            var pagedViewModel = new PagedViewModel<SerieViewModel>
            {
                Model = serieViewModels,
                Search = search,
                Page = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)pagedSerie.TotalCount / pageSize),
                TotalItems = pagedSerie.TotalCount
            };
            return View(pagedViewModel);
        }

        public async Task<IActionResult> UpsertPartial(Guid instituicaoId, Guid? id)
        {
            var userId = Guid.Parse(_userManager.GetUserId(User));

            if (!await _instituicaoService.VerificaUsuarioInstituicao(userId, instituicaoId))
                return NotFound();

            SerieViewModel serieViewModel;

            if (id.HasValue)
            {
                var serie = await _serieService.ObterPorId(id.Value);
                if (serie == null) return NotFound();

                serieViewModel = _mapper.Map<SerieViewModel>(serie);
            }
            else
            {
                serieViewModel = new SerieViewModel
                {
                    InstituicaoId = instituicaoId
                };
            }

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_UpsertPartial", serieViewModel);
            }

            return View(serieViewModel);
        }

        public async Task<IActionResult> Upsert(Guid instituicaoId, Guid? id)
        {
            var userId = Guid.Parse(_userManager.GetUserId(User));

            if (!await _instituicaoService.VerificaUsuarioInstituicao(userId, instituicaoId))
                return NotFound();

            SerieViewModel SerieViewModel;

            if (id.HasValue)
            {
                var Serie = await _serieService.ObterPorId(id.Value);
                if (Serie == null) return NotFound();

                SerieViewModel = _mapper.Map<SerieViewModel>(Serie);
            }
            else
            {
                SerieViewModel = new SerieViewModel
                {
                    InstituicaoId = instituicaoId
                };
            }

            return View(SerieViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(SerieViewModel SerieViewModel)
        {
            var userId = Guid.Parse(_userManager.GetUserId(User));
            if (!await _instituicaoService.VerificaUsuarioInstituicao(userId, SerieViewModel.InstituicaoId))
                return NotFound();

            if (!ModelState.IsValid) return View(SerieViewModel);

            if (SerieViewModel.Id == Guid.Empty || SerieViewModel.Id == null)
            {
                await _serieService.Adicionar(_mapper.Map<Serie>(SerieViewModel));
            }
            else
            {
                var SerieExistente = await _serieService.ObterPorId((Guid)SerieViewModel.Id);
                if (SerieExistente == null) return NotFound();

                _mapper.Map(SerieViewModel, SerieExistente);
                await _serieService.Atualizar(SerieExistente);
            }

            return OperacaoValida()
                ? RedirectToAction(nameof(Upsert), new { instituicaoId = SerieViewModel.InstituicaoId })
                : View(SerieViewModel);
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

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
