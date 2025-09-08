using AutoMapper;
using FluentValidation;
using GuedesTime.Domain.Intefaces;
using GuedesTime.Domain.Models;
using GuedesTime.Domain.Models.Enums;
using GuedesTime.MVC.Extensions;
using GuedesTime.MVC.Models;
using GuedesTime.MVC.ViewModels;
using GuedesTime.MVC.ViewModels.Enum;
using GuedesTime.MVC.ViewModels.Utils;
using GuedesTime.MVC.ViewModels.Validations;
using GuedesTime.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;

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

        public async Task<IActionResult> Index(
            string? search,
            Guid? professor,
            Guid? serie,
            bool? ativo = true,
            int? page = 1,
            int? pageSize = ((int)EnumQuantidadeDeItensPorPagina.Muitos))
        {
            var userId = Guid.Parse(_userManager.GetUserId(User));
            var instituicaoId = Guid.Parse(HttpContext.Session.GetString("InstituicaoId"));

            if (!await _instituicaoService.VerificaUsuarioInstituicao(userId, instituicaoId))
                return NotFound();

            ViewBag.FiltroProfessor = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Prof. João" },
                new SelectListItem { Value = "2", Text = "Prof. Maria" },
                new SelectListItem { Value = "3", Text = "Prof. Ana" }
            };


            ViewBag.FiltroSerie = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "1ª Série" },
                new SelectListItem { Value = "2", Text = "2ª Série" },
                new SelectListItem { Value = "3", Text = "3ª Série" }
            };


            ViewBag.ProfessorSelecionado = professor?.ToString();
            ViewBag.SerieSelecionada = serie?.ToString();

            var pagedDisciplinas = await _disciplinaService.GetPagedByInstituicaoAsync(
                instituicaoId,
                search,
                page.Value,
                pageSize.Value,
                ativo.Value,
                filtroAdicional: null,
                ordenacao: q => q.OrderBy(s => s.Nome).ThenBy(s => s.Nome)
            );

            var disciplinaViewModel = _mapper.Map<IEnumerable<DisciplinaViewModel>>(pagedDisciplinas.Items);

            var pagedViewModel = new PagedViewModel<DisciplinaViewModel>
            {
                Model = disciplinaViewModel,
                Search = search,
                Page = page.Value,
                PageSize = pageSize.Value,
                TotalPages = (int)Math.Ceiling((double)pagedDisciplinas.TotalCount / pageSize.Value),
                TotalItems = pagedDisciplinas.TotalCount
            };

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_DisciplinaListPartial", pagedViewModel);
            }
            return View(pagedViewModel);
        }


        public async Task<IActionResult> ConsultaDiciplinas(Guid id, string? search, int page = 1, int pageSize = 5, bool ativo = true)
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

        [HttpGet]
        public async Task<IActionResult> Upsert(Guid? id)
        {
            DisciplinaViewModel disciplinaViewModel;

            var userId = Guid.Parse(_userManager.GetUserId(User));
            var instituicaoId = Guid.Parse(HttpContext.Session.GetString("InstituicaoId"));

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

            ViewBag.EstadoInicialAtivo = disciplinaViewModel.Ativo.GetValueOrDefault(true);

            return PartialView("_Upsert", disciplinaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(DisciplinaViewModel disciplinaViewModel)
        {
            var validator = new DisciplinaViewModelValidation();

            var validationResult = await validator.ValidateAsync(disciplinaViewModel);

            if (!validationResult.IsValid)
            {
                var erros = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();
                return Json(new { success = false, errors = erros });
            }

            var instituicaoId = Guid.Parse(HttpContext.Session.GetString("InstituicaoId"));
            disciplinaViewModel.InstituicaoId = instituicaoId;

            bool isCadastro = disciplinaViewModel.Id == Guid.Empty || disciplinaViewModel.Id == null;


            var nomesDuplicados = await _disciplinaService.VerificarDisciplinasDuplicadasAsync(
                instituicaoId,
                disciplinaViewModel.Nome,
                disciplinaViewModel.Nomes,
                isCadastro ? null : disciplinaViewModel.Id);

            if (nomesDuplicados.Any())
            {
                var mensagensErro = nomesDuplicados
                    .Select(n => $"A Disciplina '{n}' já existe para o tipo de ensino selecionado.")
                    .ToArray();
                return Json(new { success = false, errors = mensagensErro });
            }

            if (isCadastro)
                return await ProcessarCadastroAsync(disciplinaViewModel, instituicaoId);
            else
                return await ProcessarAtualizacaoAsync(disciplinaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Desativar(Guid instituicaoId, Guid id)
        {
            var userId = Guid.Parse(_userManager.GetUserId(User));
            if (!await _instituicaoService.VerificaUsuarioInstituicao(userId, instituicaoId))
            {
                return NotFound(new { success = false, message = "Permissão negada ou instituição não encontrada." });
            }

            var disciplinaExistente = await _disciplinaService.ObterPorId(id);
            if (disciplinaExistente == null)
            {
                return NotFound(new { success = false, message = "Disciplina não encontrada!" });
            }

            try
            {
                if (disciplinaExistente.Ativo.Value)
                {
                    disciplinaExistente.Ativo = false;
                    await _disciplinaService.Atualizar(disciplinaExistente);
                    return Json(new { success = true, message = "Disciplina desativada com sucesso!" });
                }
                else
                {
                    await _disciplinaService.Remover(id);
                    return Json(new { success = true, message = "Disciplina removida permanentemente com sucesso!" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Ocorreu um erro interno ao processar sua solicitação." });
            }
        }

        #region Funções de Apoio

        private async Task<IActionResult> ProcessarCadastroAsync(DisciplinaViewModel disciplinaViewModel, Guid instituicaoId)
        {
            var nomesDisciplinas = disciplinaViewModel.Nomes?
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(n => n.Trim())
                .Where(n => !string.IsNullOrEmpty(n))
                .ToList();

            if (nomesDisciplinas == null || !nomesDisciplinas.Any())
                return Json(new { success = false, errors = new[] { "Nenhuma Disciplina válida informada." } });

            var viewModels = nomesDisciplinas.Select(nome => new DisciplinaViewModel
            {
                Nome = nome,
                InstituicaoId = instituicaoId,
                Ativo = disciplinaViewModel.Ativo ?? true,
                Codigo = null,
                DataCriacao = DateTime.UtcNow
            }).ToList();

            var entidades = _mapper.Map<List<Disciplina>>(viewModels);

            await _disciplinaService.AdicionarVariasAsync(entidades);

            TempData["success"] = "Séries cadastradas com sucesso!";
            return Json(new { success = true, url = Url.Action("index", "Disciplina") });
        }

        private async Task<IActionResult> ProcessarAtualizacaoAsync(DisciplinaViewModel disciplinaViewModel)
        {
            var serieExiste = await _disciplinaService.ObterPorId((Guid)disciplinaViewModel.Id);
            if (serieExiste == null)
                return Json(new { success = false, errors = new[] { "Série não encontrada." } });

            disciplinaViewModel.Codigo = serieExiste.Codigo ?? 0;

            _mapper.Map(disciplinaViewModel, serieExiste);
            await _disciplinaService.Atualizar(serieExiste);

            TempData["success"] = "Dados da Série alterados com sucesso!";

            if (!OperacaoValida())
                return Json(new { success = false, errors = ObterErrosDeNegocio() });

            return Json(new { success = true, url = Url.Action("index", "Disciplina") });
        }

        #endregion
    }
}