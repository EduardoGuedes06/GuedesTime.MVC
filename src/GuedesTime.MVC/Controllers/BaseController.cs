using GuedesTime.Domain.Enums;
using GuedesTime.Domain.Intefaces;
using GuedesTime.MVC.Models;
using GuedesTime.Service.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GuedesTime.MVC.Controllers
{
	public abstract class BaseController : Controller
	{
		private readonly INotificador _notificador;

		protected BaseController(INotificador notificador)
		{
			_notificador = notificador;
		}

		protected bool OperacaoValida()
		{
			return !_notificador.TemNotificacao();
		}

		protected IEnumerable<string> ObterErrosDeNegocio()
		{
			return _notificador.ObterNotificacoes().Select(n => n.Mensagem);
		}

		public override void OnActionExecuting(ActionExecutingContext context)
		{
			var controllerName = context.RouteData.Values["controller"]?.ToString();
			var controllersPermitidos = new[] { "Instituicao", "Home", "Account" };
			var controllersAValidarAcesso = new[] { "Serie", "Disciplina", "Professor" };

			if (User.Identity.IsAuthenticated && !controllersPermitidos.Contains(controllerName))
			{
				var instituicaoIdStr = context.HttpContext.Session.GetString("InstituicaoId");

				if (string.IsNullOrEmpty(instituicaoIdStr) || !Guid.TryParse(instituicaoIdStr, out var instituicaoId))
				{
					context.Result = new RedirectToActionResult("SelecionarInstituicao", "Instituicao", null);
					return;
				}

				if (controllersAValidarAcesso.Contains(controllerName) &&
					!ValidarAcessoInstituicaoViaRota(context.HttpContext))
				{
					TempData["error"] = "Você não tem acesso a essa instituição!";
					context.Result = new RedirectToActionResult("SelecionarInstituicao", "Instituicao", null);
					return;
				}
			}

			base.OnActionExecuting(context);
		}

		protected bool ResponsePossuiErros(ResponseResult resposta)
		{
			if (resposta != null && resposta.Errors.Mensagens.Any())
			{
				foreach (var mensagem in resposta.Errors.Mensagens)
				{
					ModelState.AddModelError(string.Empty, mensagem);
				}
				return true;
			}
			return false;
		}

		#region Utils
		protected bool ValidarNomesMultiplos(string nomesMultiplos)
		{
			var maxLength = (int)ValidacaoMultiplosInputsEnum.MaxLengthNome;

			var nomes = nomesMultiplos
				.Split(',', StringSplitOptions.RemoveEmptyEntries)
				.Select(n => n.Trim())
				.Where(n => !string.IsNullOrWhiteSpace(n))
				.ToList();

			var nomesDuplicados = nomes
				.GroupBy(n => n, StringComparer.OrdinalIgnoreCase)
				.Where(g => g.Count() > 1)
				.Select(g => g.Key)
				.ToList();

			var nomesInvalidos = nomes
				.Where(nome =>
					string.IsNullOrWhiteSpace(nome) ||
					nome.Length > maxLength ||
					!char.IsUpper(nome[0]))
				.ToList();

			if (!nomesInvalidos.Any() && !nomesDuplicados.Any())
				return true;

			if (nomesDuplicados.Any())
			{
				TempData["error"] = $"Não é permitido inserir nomes repetidos: {string.Join(", ", nomesDuplicados)}.";
				return false;
			}

			if (nomesInvalidos.Any())
			{
				TempData["error"] = $"Os seguintes nomes são inválidos (devem começar com letra maiúscula e ter até {maxLength} caracteres): {string.Join(", ", nomesInvalidos)}.";
				return false;
			}

			return true;
		}

		private bool ValidarAcessoInstituicaoViaRota(HttpContext httpContext)
		{
			try
			{
				var request = httpContext.Request;
				var baseUrl = $"{request.Scheme}://{request.Host}";
				using var httpClient = new HttpClient();
				if (request.Headers.TryGetValue("Cookie", out var cookieHeader))
				{
					httpClient.DefaultRequestHeaders.Add("Cookie", cookieHeader.ToString());
				}
				var response = httpClient.GetAsync($"{baseUrl}/Instituicao/ValidaUsuarioInstituicao").Result;
				var content = response.Content.ReadAsStringAsync().Result;

				return response.IsSuccessStatusCode && bool.TryParse(content, out var result) && result;
			}
			catch
			{
				return false;
			}
		}


		#endregion
	}
}