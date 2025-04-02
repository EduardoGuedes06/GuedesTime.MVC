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

        public IEnumerable<PlanejamentoDeAulaViewModel> PlanejamentosDeAula { get; set; }
    }
}