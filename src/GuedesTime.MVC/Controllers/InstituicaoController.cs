using AutoMapper;
using GuedesTime.Domain.Intefaces;
using GuedesTime.Domain.Models;
using GuedesTime.MVC.Models;
using GuedesTime.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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


        public async Task<IActionResult> Index()
        {
            var UserId = Guid.Parse(_userManager.GetUserId(User));

            var instituicoes = await _instituicaoService.ObterDadosInstituicoesUsuario(UserId);
            return View(instituicoes);
        }

        // DETALHES
        public async Task<IActionResult> Details(Guid id)
        {
            var instituicao = await _instituicaoService.ObterPorId(id);
            var instituicoesViewModel = _mapper.Map<IEnumerable<InstituicaoViewModel>>(instituicao);

            if (instituicoesViewModel == null) return NotFound();
            return View(instituicoesViewModel);
        }

        // EXIBIR FORMULÁRIO DE CRIAÇÃO
        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(Guid? id)
        {
            InstituicaoViewModel instituicaoViewModel = new();

            if (id.HasValue)
            {
                var instituicao = await _instituicaoService.ObterInstituicaoComEnderecoPorId(id.Value);
                if (instituicao == null) return NotFound();
                instituicaoViewModel = _mapper.Map<InstituicaoViewModel>(instituicao);

                instituicaoViewModel.Endereco.Cep = instituicaoViewModel.Endereco.Cep.Replace("-", "");

            }

            return View(instituicaoViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(InstituicaoViewModel instituicaoViewModel)
        {
            if (!ModelState.IsValid) return View(instituicaoViewModel);

            instituicaoViewModel.UsuarioId = Guid.Parse(_userManager.GetUserId(User));

            if (instituicaoViewModel.Id == Guid.Empty)
            {
                instituicaoViewModel.Avatar = await _instituicaoService.ObterAvatarAleatorioAsync();
                await _instituicaoService.Adicionar(_mapper.Map<Instituicao>(instituicaoViewModel));
            }
            else
            {
                var instituicaoExistente = await _instituicaoService.ObterPorId(instituicaoViewModel.Id);
                if (instituicaoExistente == null) return NotFound();

                _mapper.Map(instituicaoViewModel, instituicaoExistente);
                await _instituicaoService.Atualizar(instituicaoExistente);
            }

            return OperacaoValida() ? RedirectToAction(nameof(Index)) : View(instituicaoViewModel);
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

        // EXCLUIR INSTITUIÇÃO
        public async Task<IActionResult> Delete(Guid id)
        {
            var instituicao = await _instituicaoService.ObterPorId(id);
            if (instituicao == null) return NotFound();

            return View(_mapper.Map<InstituicaoViewModel>(instituicao));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _instituicaoService.Remover(id);

            if (!OperacaoValida()) return View();

            return RedirectToAction(nameof(Index));
        }

    }
}
