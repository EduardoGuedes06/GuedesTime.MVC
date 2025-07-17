using FluentValidation;
using FluentValidation.Results;
using GuedesTime.Domain.Models.Enums;
using GuedesTime.MVC.ViewModels.Enum;
using System.Collections.Generic;
using System.Linq;

namespace GuedesTime.MVC.ViewModels.Validations
{
	public class SerieViewModelValidation : AbstractValidator<SerieViewModel>
	{
		public SerieViewModelValidation()
		{

			RuleFor(s => s)
				.Must(s => !string.IsNullOrWhiteSpace(s.SerieUnica) || !string.IsNullOrWhiteSpace(s.SeriesMultiplas))
				.WithMessage("Para registrar, preencha o campo 'Série' (para um cadastro) ou 'Múltiplas Séries' (para vários).");

			RuleFor(s => s)
				.Must(s => string.IsNullOrWhiteSpace(s.SerieUnica) || string.IsNullOrWhiteSpace(s.SeriesMultiplas))
				.WithMessage("Escolha um modo de cadastro: preencha 'Série' ou 'Múltiplas Séries', mas não os dois ao mesmo tempo.");

			RuleFor(s => s.TipoEnsino)
				.IsInEnum()
				.WithMessage("Por favor, selecione um tipo de ensino válido.");


			RuleFor(s => s.SerieUnica)
				.Must((model, serieUnica) => ValidarNomeSerie(serieUnica, model.TipoEnsino))
				.When(s => !string.IsNullOrWhiteSpace(s.SerieUnica) && string.IsNullOrWhiteSpace(s.SeriesMultiplas))
				.WithMessage((model, serieUnica) =>
				{
					var validos = ObterListaDeValidos(model.TipoEnsino);
					var seriesValidasTexto = string.Join(", ", validos);
					return $"A série '{serieUnica}' não é válida para {ObterNomeTipoEnsino(model.TipoEnsino)}. As séries aceitas são: {seriesValidasTexto}.";
				});

			RuleFor(s => s.SeriesMultiplas)
				.Custom(ValidarSeriesMultiplasDetalhadas)
				.When(s => !string.IsNullOrWhiteSpace(s.SeriesMultiplas) && string.IsNullOrWhiteSpace(s.SerieUnica));
		}

		private void ValidarSeriesMultiplasDetalhadas(string seriesMultiplas, ValidationContext<SerieViewModel> context)
		{
			var model = context.InstanceToValidate;
			var series = seriesMultiplas.Split(',', System.StringSplitOptions.RemoveEmptyEntries)
										.Select(s => LimparSerie(s))
										.ToList();

			if (!series.Any())
			{
				context.AddFailure("O campo 'Múltiplas Séries' não pode estar vazio ou conter apenas espaços e vírgulas.");
				return;
			}

			var duplicados = series.GroupBy(s => s)
								   .Where(g => g.Count() > 1)
								   .Select(g => g.Key)
								   .ToList();

			if (duplicados.Any())
			{
				var duplicadosTexto = string.Join(", ", duplicados.Select(d => $"'{d}'"));
				context.AddFailure($"Você inseriu séries duplicadas: {duplicadosTexto}. Por favor, remova as repetições.");
				return;
			}

			var valoresValidos = ObterListaDeValidos(model.TipoEnsino);
			var seriesInvalidas = series.Where(s => !valoresValidos.Contains(s)).ToList();

			if (seriesInvalidas.Any())
			{
				var invalidasTexto = string.Join(", ", seriesInvalidas.Select(i => $"'{i}'"));
				var nomeEnsino = ObterNomeTipoEnsino(model.TipoEnsino);
				var validosTexto = string.Join(", ", valoresValidos);

				if (seriesInvalidas.Count == 1)
				{
					context.AddFailure($"A série {invalidasTexto} não é válida para {nomeEnsino}. As séries aceitas são: {validosTexto}.");
				}
				else
				{
					context.AddFailure($"As seguintes séries são inválidas para {nomeEnsino}: {invalidasTexto}. As séries aceitas são: {validosTexto}.");
				}
			}
		}

		private string LimparSerie(string nomeSerie)
		{
			return nomeSerie.Replace("ano", "")
							.Replace("ª", "º")
							.Replace("do Ensino Médio", "")
							.Trim();
		}

		private IEnumerable<string> ObterListaDeValidos(EnumTipoEnsinoViewModel tipoEnsino)
		{
			return tipoEnsino switch
			{
				EnumTipoEnsinoViewModel.Ensino_Infantil => new List<string> { "1º", "2º", "3º" },
				EnumTipoEnsinoViewModel.Ensino_Fundamental_I => GerarOrdinais(1, 5, "º"),
				EnumTipoEnsinoViewModel.Ensino_Fundamental_II => GerarOrdinais(6, 4, "º"),
				EnumTipoEnsinoViewModel.Ensino_Medio => GerarOrdinais(1, 3, "º"),
				_ => Enumerable.Empty<string>()
			};
		}

		private bool ValidarNomeSerie(string nomeSerie, EnumTipoEnsinoViewModel tipoEnsino)
		{
			if (string.IsNullOrWhiteSpace(nomeSerie)) return false;
			var serieLimpa = LimparSerie(nomeSerie);
			var valoresValidos = ObterListaDeValidos(tipoEnsino);
			return valoresValidos.Contains(serieLimpa);
		}

		private IEnumerable<string> GerarOrdinais(int inicio, int quantidade, string sufixo)
		{
			return Enumerable.Range(inicio, quantidade).Select(i => $"{i}{sufixo}");
		}

		private string ObterNomeTipoEnsino(EnumTipoEnsinoViewModel tipoEnsino)
		{
			return tipoEnsino switch
			{
				EnumTipoEnsinoViewModel.Ensino_Infantil => "Educação Infantil",
				EnumTipoEnsinoViewModel.Ensino_Fundamental_I => "Ensino Fundamental I",
				EnumTipoEnsinoViewModel.Ensino_Fundamental_II => "Ensino Fundamental II",
				EnumTipoEnsinoViewModel.Ensino_Medio => "Ensino Médio",
				_ => "Tipo de Ensino Desconhecido"
			};
		}
	}
}