using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using GuedesTime.MVC.Interfaces;
using GuedesTime.MVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace GuedesTime.MVC.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSenderGripGrip _emailSender;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSenderGripGrip emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }


        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

		public class InputModel
		{
			[Required(ErrorMessage = "O campo {0} é obrigatório.")]
			[EmailAddress(ErrorMessage = "O campo {0} não está em um formato válido.")]
			[Display(Name = "Email")]
			public string Email { get; set; }

			[Required(ErrorMessage = "O campo {0} é obrigatório.")]
			[StringLength(100, ErrorMessage = "A {0} deve ter no mínimo {2} e no máximo {1} caracteres.", MinimumLength = 6)]
			[DataType(DataType.Password)]
			[Display(Name = "Senha")]
			public string Password { get; set; }

			[DataType(DataType.Password)]
			[Display(Name = "Confirme a senha")]
			[Compare("Password", ErrorMessage = "A senha e a confirmação de senha não são iguais.")]
			public string ConfirmPassword { get; set; }
		}

		public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

		public async Task<IActionResult> OnPostAsync(string returnUrl = null)
		{
			returnUrl ??= Url.Content("~/");
			ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

			if (!ModelState.IsValid)
			{
				TempData["error"] = "Dados inválidos. Por favor, verifique os erros no formulário.";
				return Page();
			}

			var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email };
			var result = await _userManager.CreateAsync(user, Input.Password);

			if (result.Succeeded)
			{
				_logger.LogInformation("User created a new account with password.");

				var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
				code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
				var callbackUrl = Url.Page(
					"/Account/ConfirmEmail",
					pageHandler: null,
					values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
					protocol: Request.Scheme);

				await _emailSender.SendEmailAsync(Input.Email, "Confirme seu e-mail",
					$"Por favor, confirme sua conta <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicando aqui</a>.");

				if (_userManager.Options.SignIn.RequireConfirmedAccount)
				{
					TempData["info"] = "Conta criada! Um link de confirmação foi enviado para o seu e-mail.";
					return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
				}
				else
				{
					await _signInManager.SignInAsync(user, isPersistent: false);

					TempData["success"] = "Conta criada com sucesso! Bem-vindo.";
					return LocalRedirect(returnUrl);
				}
			}

			foreach (var error in result.Errors)
			{
				ModelState.AddModelError(string.Empty, error.Description);
			}

			return Page();
		}
	}
}
