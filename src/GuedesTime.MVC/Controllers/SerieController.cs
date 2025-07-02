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

		public ActionResult Index()
		{
			return View();
		}

		public async Task<IActionResult> IndexAntigo(Guid id, string? search, int page = 1, int pageSize = 5, bool ativo = true)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(SerieViewModel serieViewModel)
        {
			var referer = Request.Headers["Referer"].ToString();
			var userId = Guid.Parse(_userManager.GetUserId(User));
			if (!await _instituicaoService.VerificaUsuarioInstituicao(userId, serieViewModel.InstituicaoId))
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

				return View(serieViewModel);
			}

			var serieComMesmoNome = await _serieService.ObterSeriePorNome(serieViewModel.InstituicaoId, serieViewModel.Nome);


			if (serieComMesmoNome != null &&
				(serieViewModel.Id == null || serieComMesmoNome.Id != serieViewModel.Id) &&
				serieComMesmoNome.Ativo == serieViewModel.Ativo)
			{
				TempData["error"] = "Já existe uma serie com esse nome!";
				return Redirect(referer);
			}

			if (serieViewModel.Id == null || serieViewModel.Id == Guid.Empty)
			{
				var novaserie = _mapper.Map<Serie>(serieViewModel);
				await _serieService.Adicionar(novaserie);
				TempData["success"] = "serie cadastrada com sucesso!";
			}
			else
			{
				var serieExistente = await _serieService.ObterPorId(serieViewModel.Id);
				if (serieExistente == null)
					return NotFound();

				_mapper.Map(serieViewModel, serieExistente);
				await _serieService.Atualizar(serieExistente);
				TempData["success"] = "Dados da serie alterados com sucesso!";
			}

			if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
			{
				return Json(new { success = true });
			}

			if (!string.IsNullOrEmpty(referer))
			{
				return Redirect(referer);
			}

			return RedirectToAction("Detalhes", "Instituicao", new { id = serieViewModel.InstituicaoId });
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(Guid instituicaoId, Guid id)
		{
			var userId = Guid.Parse(_userManager.GetUserId(User));
			if (!await _instituicaoService.VerificaUsuarioInstituicao(userId, instituicaoId))
				return NotFound();

			var client = await _serieService.ObterPorId(id);
			if (client == null)
			{
				TempData["warning"] = "Serie não encontrada!!";
				return NotFound();
			}

			var serieExistente = await _serieService.ObterPorId((Guid)id);
			if (serieExistente == null) return NotFound();

			if (serieExistente.Ativo == true)
			{ serieExistente.Ativo = false; await _serieService.Atualizar(serieExistente); TempData["success"] = "Serie desativada com sucesso!!"; }
			else { await _serieService.Remover(id); TempData["success"] = "Serie Deletada do sistema com sucesso!!"; }

			var referer = Request.Headers["Referer"].ToString();
			if (!string.IsNullOrEmpty(referer))
			{
				return Redirect(referer);
			}
			return Ok();
		}
	}
}
