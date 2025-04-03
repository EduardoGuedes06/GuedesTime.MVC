using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using GuedesTime.MVC.Extensions;
using Microsoft.AspNetCore.Http;

namespace GuedesTime.MVC.ViewModels
{
    public class InstituicaoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Nome da Instituição")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 3)]
        public string Nome { get; set; }

        [DisplayName("Codigo INEP / Codigo CIE")]
        [Required]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "O INEP deve ter 8 dígitos.")]

        public string CodigoCie { get; set; }
        [DisplayName("CNPJ")]
        [StringLength(18, ErrorMessage = "O campo {0} deve ter {1} caracteres")]
        public string? Cnpj { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Usuário Responsável")]
        public Guid UsuarioId { get; set; }

        [DisplayName("Avatar")]

        public string? Avatar { get; set; }

        public bool? Ativo { get; set; }

        public EnderecoViewModel Endereco { get; set; }
        public ICollection<ProfessorViewModel>? Professores { get; set; }
        public ICollection<TurmaViewModel>? Turmas { get; set; }
        public ICollection<DisciplinaViewModel>? Disciplinas { get; set; }
        public ICollection<SalaViewModel>? Salas { get; set; }
        public ICollection<HorarioViewModel>? Horarios { get; set; }
        public ICollection<FeriadoViewModel>? Feriados { get; set; }
    }

}