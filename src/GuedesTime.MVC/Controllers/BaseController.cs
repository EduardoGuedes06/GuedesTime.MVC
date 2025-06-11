using GuedesTime.Domain.Enums;
using GuedesTime.Domain.Intefaces;
using GuedesTime.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace GuedesTime.MVC.Controllers
{
    public abstract class BaseController : Controller
    {
        public readonly INotificador _notificador;

        internal BaseController(INotificador notificador)
        {
            _notificador = notificador;
        }

        internal bool OperacaoValida()
        {
            return !_notificador.TemNotificacao();
        }

        internal bool ResponsePossuiErros(ResponseResult resposta)
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
		internal bool ValidarNomesMultiplos(string nomesMultiplos)
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
		#endregion
	}
}