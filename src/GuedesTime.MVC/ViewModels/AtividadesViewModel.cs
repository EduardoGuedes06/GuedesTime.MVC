using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using GuedesTime.MVC.Extensions;
using Microsoft.AspNetCore.Http;

namespace GuedesTime.MVC.ViewModels
{
    public class AtividadesViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Descricao { get; set; }

        [DisplayName("Data Início")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime DataInicio { get; set; }

        [DisplayName("Data Fim")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime DataFim { get; set; }

		[DisplayName("Data do Cadastro")]
		public DateTime DataCriacao { get; set; }


		[DisplayName("Data da Alteração")]
		public DateTime? DataAlteracao { get; set; }


		[DisplayName("Ativo")]
		public bool? Ativo { get; set; }
		public Guid ProfessorId { get; set; }
        public ProfessorViewModel Professor { get; set; }
    }
}