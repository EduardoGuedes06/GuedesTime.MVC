using AutoMapper;
using GuedesTime.Domain.Intefaces;
using GuedesTime.MVC.Models;
using GuedesTime.MVC.Utils;
using GuedesTime.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace GuedesTime.MVC.Controllers
{
    [Authorize]
    public class UsuarioController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IInstituicaoService _instituicaoService;
        private readonly IWebHostEnvironment _env;

        public UsuarioController(IMapper mapper, 
        INotificador notificador,
        UserManager<ApplicationUser> userManager,
        IInstituicaoService instituicaoService,
        IWebHostEnvironment env) : base(notificador)
        {
            _mapper = mapper;
            _userManager = userManager;
            _instituicaoService = instituicaoService;
            _env = env;
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
                AvatarUrl = string.IsNullOrWhiteSpace(user.Imagem) ? "/img/userPlaceholder.png" : user.Imagem
            };

            // Avatares pré-carregados (se existirem em wwwroot/assets/avatar)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AtualizarDados(string nome, string? cpf)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user is null) return Challenge();

            nome = (nome ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(nome))
            {
                TempData["error"] = "Nome é obrigatório.";
                return RedirectToAction(nameof(Index));
            }

            var cpfDigits = CpfUtils.OnlyDigits(cpf ?? string.Empty);
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

            // segurança: somente arquivo existente dentro de wwwroot/assets/avatar
            var fileName = Path.GetFileName(avatarUrl);
            var physical = Path.Combine(_env.WebRootPath, "assets", "avatar", fileName);
            if (!System.IO.File.Exists(physical))
            {
                TempData["error"] = "Avatar não encontrado.";
                return RedirectToAction(nameof(Index));
            }

            user.Imagem = $"/assets/avatar/{fileName}";
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

            if (string.IsNullOrWhiteSpace(dataUrl) || !dataUrl.StartsWith("data:image/", StringComparison.OrdinalIgnoreCase))
            {
                TempData["error"] = "Imagem inválida.";
                return RedirectToAction(nameof(Index));
            }

            var commaIdx = dataUrl.IndexOf(',');
            if (commaIdx < 0)
            {
                TempData["error"] = "Imagem inválida.";
                return RedirectToAction(nameof(Index));
            }

            var header = dataUrl.Substring(0, commaIdx);
            var base64 = dataUrl.Substring(commaIdx + 1);

            var ext = header.Contains("image/png", StringComparison.OrdinalIgnoreCase) ? "png"
                : header.Contains("image/jpeg", StringComparison.OrdinalIgnoreCase) ? "jpg"
                : header.Contains("image/jpg", StringComparison.OrdinalIgnoreCase) ? "jpg"
                : null;

            if (ext is null)
            {
                TempData["error"] = "Formato não suportado (use PNG ou JPG).";
                return RedirectToAction(nameof(Index));
            }

            byte[] bytes;
            try
            {
                bytes = Convert.FromBase64String(base64);
            }
            catch
            {
                TempData["error"] = "Imagem inválida (base64).";
                return RedirectToAction(nameof(Index));
            }

            // limite simples (1MB)
            if (bytes.Length > 1_000_000)
            {
                TempData["error"] = "Imagem muito grande (máx. 1MB).";
                return RedirectToAction(nameof(Index));
            }

            var uploadDir = Path.Combine(_env.WebRootPath, "uploads", "avatars");
            Directory.CreateDirectory(uploadDir);

            var fileName = $"{user.Id}.{ext}";
            var path = Path.Combine(uploadDir, fileName);
            await System.IO.File.WriteAllBytesAsync(path, bytes);

            user.Imagem = $"/uploads/avatars/{fileName}";
            var res = await _userManager.UpdateAsync(user);
            if (!res.Succeeded)
            {
                TempData["error"] = string.Join("; ", res.Errors.Select(e => e.Description));
                return RedirectToAction(nameof(Index));
            }

            TempData["success"] = "Avatar atualizado.";
            return RedirectToAction(nameof(Index));
        }

        // GET: ConfiguracoesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ConfiguracoesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ConfiguracoesController/Create
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

        // GET: ConfiguracoesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ConfiguracoesController/Edit/5
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

        // GET: ConfiguracoesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ConfiguracoesController/Delete/5
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
