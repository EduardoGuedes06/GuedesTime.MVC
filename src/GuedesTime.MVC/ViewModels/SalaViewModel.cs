using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using GuedesTime.MVC.Extensions;
using Microsoft.AspNetCore.Http;

namespace GuedesTime.MVC.ViewModels
{
    public class SalaViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }

        [DisplayName("Capacidade")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Capacidade { get; set; }

        public IEnumerable<PlanejamentoDeAulaViewModel> PlanejamentosDeAula { get; set; }
    }
}