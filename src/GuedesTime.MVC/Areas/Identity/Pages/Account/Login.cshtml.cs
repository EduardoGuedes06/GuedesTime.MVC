using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using GuedesTime.MVC.Models;

namespace GuedesTime.MVC.Areas.Identity.Pages.Account
{
	[AllowAnonymous]
	public class LoginModel : PageModel
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly ILogger<LoginModel> _logger;

		public LoginModel(SignInManager<ApplicationUser> signInManager,
			ILogger<LoginModel> logger,
			UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_logger = logger;
		}

		[BindProperty]
		public InputModel Input { get; set; }

		public IList<AuthenticationScheme> ExternalLogins { get; set; }

		public string ReturnUrl { get; set; }

		[TempData]
		public string ErrorMessage { get; set; }

		public class InputModel
		{
			[Required(ErrorMessage = "O campo {0} é obrigatório.")]
			[EmailAddress(ErrorMessage = "O campo {0} não está em um formato válido.")]
			public string Email { get; set; }

			[Required(ErrorMessage = "O campo {0} é obrigatório.")]
			[StringLength(100, ErrorMessage = "A {0} deve ter no mínimo {2} e no máximo {1} caracteres.", MinimumLength = 6)]
			[Display(Name = "Senha")]
			public string Password { get; set; }

			[Display(Name = "Remember me?")]
			public bool RememberMe { get; set; }
		}

		public async Task OnGetAsync(string returnUrl = null)
		{
			if (!string.IsNullOrEmpty(ErrorMessage))
			{
				ModelState.AddModelError(string.Empty, ErrorMessage);
			}

			returnUrl ??= Url.Content("~/");
			await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

			ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

			ReturnUrl = returnUrl;
		}

		public async Task<IActionResult> OnPostAsync(string returnUrl = null)
		{
			returnUrl ??= Url.Content("~/Painel");

			if (ModelState.IsValid)
			{
				var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
				if (result.Succeeded)
				{
					TempData["success"] = "Login realizado com sucesso! Bem-vindo.";
					return LocalRedirect(returnUrl);
				}
				if (result.RequiresTwoFactor)
				{
					TempData["info"] = "Sua conta requer autenticação de dois fatores.";
					return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
				}
				if (result.IsLockedOut)
				{
					TempData["warning"] = "Esta conta foi temporariamente bloqueada por excesso de tentativas.";
					return RedirectToPage("./Lockout");
				}
				else
				{
					ModelState.AddModelError(string.Empty, "Tentativa de login inválida.");
					TempData["error"] = "Email ou senha inválidos.";
					return Page();
				}
			}
			else
			{
				TempData["error"] = "Dados Invalidos!.";
				return Page();
			}

		}
	}
}
