using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using GuedesTime.MVC.Extensions;
using Microsoft.AspNetCore.Http;

namespace GuedesTime.MVC.ViewModels
{
    public class LogViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime DataHora { get; set; }
        public string Acao { get; set; }
        public string Usuario { get; set; }
        public string Detalhes { get; set; }
		[DisplayName("Data do Cadastro")]
		public DateTime DataCriacao { get; set; }


		[DisplayName("Data da Alteração")]
		public DateTime? DataAlteracao { get; set; }


		[DisplayName("Ativo")]
		public bool? Ativo { get; set; }
	}
}