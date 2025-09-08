using FluentValidation;
using FluentValidation.Results;
using System.Linq;

namespace GuedesTime.MVC.ViewModels.Validations
{
    public class DisciplinaViewModelValidation : AbstractValidator<DisciplinaViewModel>
    {
        public DisciplinaViewModelValidation()
        {
            RuleFor(s => s)
                .Must(s => !string.IsNullOrWhiteSpace(s.Nome) || !string.IsNullOrWhiteSpace(s.Nomes))
                .WithMessage("Para registrar, preencha o campo 'Nome' (para um cadastro) ou 'Nomes das Disciplinas' (para vários).");

            RuleFor(s => s)
                .Must(s => string.IsNullOrWhiteSpace(s.Nome) || string.IsNullOrWhiteSpace(s.Nomes))
                .WithMessage("Escolha um modo de cadastro: preencha 'Nome' OU 'Nomes das Disciplinas', mas não ambos.");

            RuleFor(s => s.Nome)
                .NotEmpty()
                .When(s => string.IsNullOrWhiteSpace(s.Nomes))
                .WithMessage("O campo 'Nome' da disciplina é obrigatório.");


            RuleFor(s => s.Nomes)
                .Custom(ValidarDisciplinasMultiplas)
                .When(s => string.IsNullOrWhiteSpace(s.Nome));
        }

        private void ValidarDisciplinasMultiplas(string nomes, ValidationContext<DisciplinaViewModel> context)
        {
            var disciplinas = nomes.Split(',', System.StringSplitOptions.RemoveEmptyEntries)
                                   .Select(n => n.Trim())
                                   .Where(n => !string.IsNullOrWhiteSpace(n))
                                   .ToList();

            if (!disciplinas.Any())
            {
                context.AddFailure("O campo 'Nomes das Disciplinas' não pode estar vazio ou conter apenas espaços e vírgulas.");
                return;
            }

            var duplicados = disciplinas.GroupBy(n => n)
                                        .Where(g => g.Count() > 1)
                                        .Select(g => g.Key)
                                        .ToList();

            if (duplicados.Any())
            {
                var duplicadosTexto = string.Join(", ", duplicados.Select(d => $"'{d}'"));
                context.AddFailure($"Você inseriu disciplinas duplicadas: {duplicadosTexto}. Por favor, remova as repetições.");
            }
        }
    }
}
