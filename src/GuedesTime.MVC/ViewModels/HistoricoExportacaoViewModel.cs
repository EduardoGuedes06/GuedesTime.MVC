using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using GuedesTime.MVC.Extensions;
using Microsoft.AspNetCore.Http;

namespace GuedesTime.MVC.ViewModels
{
    public class HistoricoExportacaoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Data de Exportação")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime DataExportacao { get; set; }

        [DisplayName("Nome do Arquivo")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string NomeArquivo { get; set; }

        [DisplayName("Usuário Responsável")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string UsuarioResponsavel { get; set; }

        public IEnumerable<PlanejamentoDeAulaViewModel> PlanejamentosDeAula { get; set; }
    }
}