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


        public async Task<IActionResult> Index(string? search, int page = 1, int pageSize = 10, bool ativo = true)
        {
            var UserId = Guid.Parse(_userManager.GetUserId(User));

            var instituicoes = await _instituicaoService.ObterInstituiceosPaginada(UserId, search, page, pageSize, ativo);

            var instituicoesViewModel = _mapper.Map<IEnumerable<InstituicaoViewModel>>(instituicoes.Items);

            var paged = new PagedInstituicoesViewModel
            {
                Instituicoes = instituicoesViewModel,
                Search = search,
                Page = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)instituicoes.TotalCount / pageSize),
                TotalItems = instituicoes.TotalCount
            };

            return View(paged);
        }


        // DETALHES
        [AllowAnonymous]
        public async Task<IActionResult> Detalhes(Guid id)
        {
            var instituicao = await _instituicaoService.ObterDadosInstituicoesPorId(id);
            var instituicoesViewModel = _mapper.Map<InstituicaoViewModel>(instituicao);

            var user = await _userManager.FindByIdAsync(instituicoesViewModel.UsuarioId.ToString());
            var userName = user?.UserName;
            ViewBag.Nome = userName;

            var mensagens = VerificarDadosInstituicao(instituicoesViewModel);
            if (mensagens.Any())
            {
                TempData["warning"] = string.Join(" | ", mensagens);
            }

            if (instituicoesViewModel == null) return NotFound();
            return View(instituicoesViewModel);
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

            }else { instituicaoViewModel.UsuarioId = null; }

            return View(instituicaoViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(InstituicaoViewModel instituicaoViewModel)
        {
            ModelState.Remove(nameof(instituicaoViewModel.UsuarioId));
            instituicaoViewModel.UsuarioId = Guid.Parse(_userManager.GetUserId(User));

            if (!ModelState.IsValid) return View(instituicaoViewModel);


            if (instituicaoViewModel.Id == Guid.Empty || instituicaoViewModel.Id == null)
            {
                instituicaoViewModel.Avatar = await _instituicaoService.ObterAvatarAleatorioAsync();
                await _instituicaoService.Adicionar(_mapper.Map<Instituicao>(instituicaoViewModel));
            }
            else
            {
                var instituicaoExistente = await _instituicaoService.ObterPorId((Guid)instituicaoViewModel.Id);
                if (instituicaoExistente == null) return NotFound();
                instituicaoViewModel.Avatar = instituicaoExistente.Avatar;


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

        // Notificar Dados Faltando
        private string VerificarDadosInstituicao(InstituicaoViewModel instituicoesViewModel)
        {
            var pendencias = new List<string>();

            if (instituicoesViewModel.Series == null || !instituicoesViewModel.Series.Any())
                pendencias.Add("Séries");
            if (instituicoesViewModel.Disciplinas == null || !instituicoesViewModel.Disciplinas.Any())
                pendencias.Add("Disciplinas");
            if (instituicoesViewModel.Turmas == null || !instituicoesViewModel.Turmas.Any())
                pendencias.Add("Turmas");
            if (instituicoesViewModel.Professores == null || !instituicoesViewModel.Professores.Any())
                pendencias.Add("Professores");
            if (instituicoesViewModel.Salas == null || !instituicoesViewModel.Salas.Any())
                pendencias.Add("Salas");
            if (instituicoesViewModel.Feriados == null || !instituicoesViewModel.Feriados.Any())
                pendencias.Add("Feriados");
            if (instituicoesViewModel.Horarios == null || !instituicoesViewModel.Horarios.Any())
                pendencias.Add("Horários");

            if (pendencias.Any())
            {
                if (pendencias.Count > 1)
                {
                    var lastItem = pendencias.Last();
                    pendencias[pendencias.Count - 1] = $"e {lastItem}";
                }

                return $"Cadastros Pendentes: {string.Join(", ", pendencias)}.";
            }

            return string.Empty;
        }



    }
}
