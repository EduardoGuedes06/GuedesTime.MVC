using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using GuedesTime.MVC.Extensions;
using Microsoft.AspNetCore.Http;

namespace GuedesTime.MVC.ViewModels
{
    public class PlanejamentoDeAulaViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Nome do Planejamento")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Instituição")]
        public Guid InstituicaoId { get; set; }

        public InstituicaoViewModel Instituicao { get; set; }

        public List<PlanejamentoDeAulaItemViewModel> Itens { get; set; } = new();
        public List<HistoricoExportacaoViewModel> Historicos { get; set; } = new();
    }


}