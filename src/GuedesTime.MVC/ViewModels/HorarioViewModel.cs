using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using GuedesTime.MVC.Extensions;
using Microsoft.AspNetCore.Http;

namespace GuedesTime.MVC.ViewModels
{
    public class HorarioViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Início")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime Inicio { get; set; }

        [DisplayName("Fim")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime Fim { get; set; }

		[DisplayName("Data do Cadastro")]
		public DateTime DataCriacao { get; set; }


		[DisplayName("Data da Alteração")]
		public DateTime? DataAlteracao { get; set; }


		[DisplayName("Ativo")]
		public bool? Ativo { get; set; }
		public IEnumerable<PlanejamentoDeAulaItemViewModel> PlanejamentosDeAulaItens { get; set; }
    }

}