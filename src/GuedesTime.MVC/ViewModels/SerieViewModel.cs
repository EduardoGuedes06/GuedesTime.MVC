using GuedesTime.Domain.Models;
using GuedesTime.MVC.Extensions;
using GuedesTime.MVC.ViewModels.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GuedesTime.MVC.ViewModels
{
    public class SerieViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Nome")]
        public string Nome { get; set; }

		[DisplayName("Série")]
		public string? SerieUnica { get; set; }

		[DisplayName("Registro de múltiplas Séries")]
		public string? SeriesMultiplas { get; set; }

		[DisplayName("Tipo Ensino")]
		public List<SelectListItem>? ListaTipoEnsino { get; set; }

		public Guid InstituicaoId { get; set; }
		public int? Codigo { get; set; }

		[DisplayName("Data do Cadastro")]
		public DateTime DataCriacao { get; set; }

		[DisplayName("Data da Alteração")]
		public DateTime? DataAlteracao { get; set; }

		[DisplayName("Ativo")]
		public bool? Ativo { get; set; }

		[DisplayName("Tipo Ensino")]
		[Required(ErrorMessage = "O campo de tipo Ensino é obrigatório")]
		public EnumTipoEnsinoViewModel TipoEnsino { get; set; }
		public InstituicaoViewModel Instituicao { get; set; }
        public ICollection<TurmaViewModel> Turmas { get; set; }
        public ICollection<DisciplinaSerie> Disciplinas { get; set; }
    }
}