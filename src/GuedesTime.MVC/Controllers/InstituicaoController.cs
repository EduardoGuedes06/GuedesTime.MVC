using AutoMapper;
using GuedesTime.Domain.Intefaces;
using GuedesTime.Domain.Models;
using GuedesTime.MVC.Models;
using GuedesTime.MVC.ViewModels;
using GuedesTime.MVC.ViewModels.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Signing;

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

            var instituicoes = await _instituicaoService.GetPagedByInstituicaoAsync(UserId, search, page, pageSize, ativo);
            var instituicaoIds = instituicoes.Items.Select(i => i.Id).ToList();
            var dadosResumo = _mapper.Map<Dictionary<Guid, DadosAgregadosInstituicaoViewModel>>(await _instituicaoService.ObterCalculoGeralDosDadosDaInstituicao(instituicaoIds));
            ViewBag.ResumoInstituicoes = dadosResumo;
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

            var progresso = VerificarPendencias(instituicoesViewModel);

            var totalEtapas = 8;
            var percentual = (progresso.EtapasConcluidas * 100) / totalEtapas;
            ViewBag.Progresso = percentual;
            ViewBag.EtapaAtual = progresso.EtapasConcluidas;
            ViewBag.ProximaEtapa = progresso.ProximaEtapa;

            if (progresso.Pendencias.Any())
            {
                TempData["warning"] = $"Cadastros Pendentes: {string.Join(", ", progresso.Pendencias)}";
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
                TempData["success"] = "Instituição Cadastrada com sucesso!!";
            }
            else
            {
                var instituicaoExistente = await _instituicaoService.ObterPorId((Guid)instituicaoViewModel.Id);
                if (instituicaoExistente == null) return NotFound();
                instituicaoViewModel.Avatar = instituicaoExistente.Avatar;


                _mapper.Map(instituicaoViewModel, instituicaoExistente);
                await _instituicaoService.Atualizar(instituicaoExistente);
                TempData["success"] = "Dados da Instituição alterados com sucesso!!";
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

        private ProgressoCadastroViewModel VerificarPendencias(InstituicaoViewModel instituicoesViewModel)
        {
            var pendencias = new List<string>();
            var totalEtapas = 8;
            string proximaEtapa = null;

            if (instituicoesViewModel.Disciplinas == null || !instituicoesViewModel.Disciplinas.Any())
            {
                pendencias.Add("Disciplinas");
                if (proximaEtapa == null) proximaEtapa = "Disciplinas";
            }
            if (instituicoesViewModel.Series == null || !instituicoesViewModel.Series.Any())
            {
                pendencias.Add("Séries");
                if (proximaEtapa == null) proximaEtapa = "Séries";
            }
            if (instituicoesViewModel.Turmas == null || !instituicoesViewModel.Turmas.Any())
            {
                pendencias.Add("Turmas");
                if (proximaEtapa == null) proximaEtapa = "Turmas";
            }
            if (instituicoesViewModel.Professores == null || !instituicoesViewModel.Professores.Any())
            {
                pendencias.Add("Professores");
                if (proximaEtapa == null) proximaEtapa = "Professores";
            }
            if (instituicoesViewModel.Salas == null || !instituicoesViewModel.Salas.Any())
            {
                pendencias.Add("Salas");
                if (proximaEtapa == null) proximaEtapa = "Salas";
            }
            if (instituicoesViewModel.Feriados == null || !instituicoesViewModel.Feriados.Any())
            {
                pendencias.Add("Feriados");
                if (proximaEtapa == null) proximaEtapa = "Feriados";
            }
            if (instituicoesViewModel.Horarios == null || !instituicoesViewModel.Horarios.Any())
            {
                pendencias.Add("Horários");
                if (proximaEtapa == null) proximaEtapa = "Horários";
            }

            var etapasConcluidas = totalEtapas - pendencias.Count;

            if (etapasConcluidas < 0)
                etapasConcluidas = 0;

            return new ProgressoCadastroViewModel
            {
                EtapasConcluidas = etapasConcluidas,
                Pendencias = pendencias,
                ProximaEtapa = proximaEtapa
            };
        }
    }
}
