using AutoMapper;
using GuedesTime.Domain.Intefaces;
using GuedesTime.MVC.Models;
using GuedesTime.MVC.Services;
using GuedesTime.MVC.Utils;
using GuedesTime.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GuedesTime.MVC.Controllers
{
    [Authorize]
    public class UsuarioController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IInstituicaoService _instituicaoService;
        private readonly IWebHostEnvironment _env;
        private readonly IUserAvatarService _userAvatarService;

        public UsuarioController(IMapper mapper, 
        INotificador notificador,
        UserManager<ApplicationUser> userManager,
        IInstituicaoService instituicaoService,
        IWebHostEnvironment env,
        IUserAvatarService userAvatarService) : base(notificador)
        {
            _mapper = mapper;
            _userManager = userManager;
            _instituicaoService = instituicaoService;
            _env = env;
            _userAvatarService = userAvatarService;
        }


        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user is null) return Challenge();

            var roles = await _userManager.GetRolesAsync(user);
            var tipoConta = roles.Count > 0 ? string.Join(", ", roles.OrderBy(r => r)) : "Usuário";

            var membroDesde = user.CreatedAt == default
                ? "—"
                : user.CreatedAt.ToLocalTime().ToString("dd 'de' MMMM 'de' yyyy", new System.Globalization.CultureInfo("pt-BR"));

            var cpfDigits = CpfUtils.OnlyDigits(user.Documento ?? string.Empty);
            var cpfMascarado = cpfDigits.Length == 11
                ? $"{cpfDigits[..3]}.{cpfDigits.Substring(3, 3)}.{cpfDigits.Substring(6, 3)}-{cpfDigits.Substring(9, 2)}"
                : string.Empty;

            var vm = new UsuarioPerfilViewModel
            {
                Nome = user.Nome ?? user.UserName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                TipoConta = tipoConta,
                MembroDesdeTexto = membroDesde,
                CpfMascarado = cpfMascarado,
                AvatarUrl = !string.IsNullOrWhiteSpace(user.AvatarDataProtected) ? Url.Action("Avatar", "Usuario", new { area = "" })!
					: string.IsNullOrWhiteSpace(user.Imagem) ? "/img/userPlaceholder.png" : user.Imagem
            };

            var avatarDir = Path.Combine(_env.WebRootPath, "assets", "avatar");
            if (Directory.Exists(avatarDir))
            {
                vm.AvataresDisponiveis = Directory.GetFiles(avatarDir)
                    .Select(f => Path.GetFileName(f))
                    .Where(n => n.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                                n.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                                n.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                                n.EndsWith(".webp", StringComparison.OrdinalIgnoreCase))
                    .OrderBy(n => n)
                    .Select(n => $"/assets/avatar/{n}")
                    .ToList();
            }

            if (Guid.TryParse(user.Id, out var userId))
            {
                var instituicoes = await _instituicaoService.ObterDadosInstituicoesUsuario(userId);

                vm.Instituicoes = instituicoes
                    .OrderBy(i => i.Nome)
                    .Select(i => new UsuarioPerfilViewModel.InstituicaoResumoVm
                    {
                        Id = i.Id,
                        Nome = i.Nome,
                        Avatar = i.Avatar ?? string.Empty
                    })
                    .ToList();
            }

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> EditModal()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user is null) return Challenge();

            var cpfDigits = CpfUtils.OnlyDigits(user.Documento ?? string.Empty);
            var cpfMascarado = cpfDigits.Length == 11
                ? $"{cpfDigits[..3]}.{cpfDigits.Substring(3, 3)}.{cpfDigits.Substring(6, 3)}-{cpfDigits.Substring(9, 2)}"
                : string.Empty;

            var vm = new UsuarioPerfilViewModel
            {
                Nome = user.Nome ?? user.UserName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                CpfMascarado = cpfMascarado
            };

            return PartialView("_edit", vm);
        }

        [HttpGet]
        public async Task<IActionResult> UploadModal()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user is null) return Challenge();

            var vm = new UsuarioPerfilViewModel
            {
                AvatarUrl = string.IsNullOrWhiteSpace(user.Imagem) ? "/img/userPlaceholder.png" : user.Imagem,
                AvataresDisponiveis = new List<string>()
            };

            var avatarDir = Path.Combine(_env.WebRootPath, "assets", "avatar");
            if (Directory.Exists(avatarDir))
            {
                vm.AvataresDisponiveis = Directory.GetFiles(avatarDir)
                    .Select(f => Path.GetFileName(f))
                    .Where(n => n.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                                n.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                                n.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                                n.EndsWith(".webp", StringComparison.OrdinalIgnoreCase))
                    .OrderBy(n => n)
                    .Select(n => $"/assets/avatar/{n}")
                    .ToList();
            }

            return PartialView("_upload", vm);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Avatar()
        {
            if (!User.Identity?.IsAuthenticated ?? true) return Unauthorized();

            var user = await _userManager.GetUserAsync(User);
            if (user is null) return Unauthorized();

            var (ok, _, bytes, contentType) = _userAvatarService.UnprotectToBytes(user.AvatarDataProtected, user.AvatarContentType);
            if (!ok) return NotFound();

            Response.Headers.CacheControl = "no-store";
            return File(bytes, contentType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AtualizarDados(UsuarioPerfilViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user is null) return Challenge();

            var nome = (model.Nome ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(nome))
            {
                TempData["error"] = "Nome é obrigatório.";
                return RedirectToAction(nameof(Index));
            }

            var cpfDigits = CpfUtils.OnlyDigits(model.CpfMascarado ?? string.Empty);
            if (!string.IsNullOrWhiteSpace(cpfDigits) && !CpfUtils.IsValid(cpfDigits))
            {
                TempData["error"] = "CPF inválido.";
                return RedirectToAction(nameof(Index));
            }

            user.Nome = nome;
            user.Documento = cpfDigits;
            var res = await _userManager.UpdateAsync(user);
            if (!res.Succeeded)
            {
                TempData["error"] = string.Join("; ", res.Errors.Select(e => e.Description));
                return RedirectToAction(nameof(Index));
            }

            TempData["success"] = "Dados atualizados.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SelecionarAvatar(string avatarUrl)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user is null) return Challenge();

            if (string.IsNullOrWhiteSpace(avatarUrl) || !avatarUrl.StartsWith("/assets/avatar/", StringComparison.OrdinalIgnoreCase))
            {
                TempData["error"] = "Avatar inválido.";
                return RedirectToAction(nameof(Index));
            }

            var fileName = Path.GetFileName(avatarUrl);
            var physical = Path.Combine(_env.WebRootPath, "assets", "avatar", fileName);
            if (!System.IO.File.Exists(physical))
            {
                TempData["error"] = "Avatar não encontrado.";
                return RedirectToAction(nameof(Index));
            }

            user.Imagem = $"/assets/avatar/{fileName}";
            user.AvatarContentType = string.Empty;
            user.AvatarDataProtected = string.Empty;
            var res = await _userManager.UpdateAsync(user);
            if (!res.Succeeded)
            {
                TempData["error"] = string.Join("; ", res.Errors.Select(e => e.Description));
                return RedirectToAction(nameof(Index));
            }

            TempData["success"] = "Avatar atualizado.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadAvatarBase64(string dataUrl)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user is null) return Challenge();

            if (string.IsNullOrWhiteSpace(dataUrl))
            {
                TempData["error"] = "Selecione uma imagem para upload.";
                return RedirectToAction(nameof(Index));
            }

			var (ok, error, contentType, protectedBase64) = _userAvatarService.ProtectFromDataUrl(dataUrl);
			if (!ok)
			{
				TempData["error"] = error;
				return RedirectToAction(nameof(Index));
			}

			user.Imagem = string.Empty;
			user.AvatarContentType = contentType;
			user.AvatarDataProtected = protectedBase64;
            var res = await _userManager.UpdateAsync(user);
            if (!res.Succeeded)
            {
                TempData["error"] = string.Join("; ", res.Errors.Select(e => e.Description));
                return RedirectToAction(nameof(Index));
            }

            TempData["success"] = "Avatar atualizado.";
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

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

        public ActionResult Edit(int id)
        {
            return View();
        }

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
