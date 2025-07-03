using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using GuedesTime.Domain.Models;
using GuedesTime.MVC.Extensions;
using Microsoft.AspNetCore.Http;

namespace GuedesTime.MVC.ViewModels
{
    public class TurmaViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(30, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 3)]
        public string Nome { get; set; }

        [DisplayName("Ano")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Ano { get; set; }

        public Guid InstituicaoId { get; set; }
        public Guid SerieId { get; set; }

		[DisplayName("Data do Cadastro")]
		public DateTime DataCriacao { get; set; }


		[DisplayName("Data da Alteração")]
		public DateTime? DataAlteracao { get; set; }


		[DisplayName("Ativo")]
		public bool? Ativo { get; set; }

		public InstituicaoViewModel Instituicao { get; set; }
        public SerieViewModel Serie { get; set; }

        public IEnumerable<PlanejamentoDeAulaViewModel> PlanejamentosDeAula { get; set; }
        public ICollection<ProfessorDisciplinaTurmaViewModel> ProfessoresDisciplinasTurmas { get; set; }
    }
}