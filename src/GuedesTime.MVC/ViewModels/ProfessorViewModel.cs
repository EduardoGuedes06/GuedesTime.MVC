using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using GuedesTime.MVC.Extensions;
using Microsoft.AspNetCore.Http;

namespace GuedesTime.MVC.ViewModels
{
    public class ProfessorViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }

        [DisplayName("E-mail")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string Email { get; set; }

        public IEnumerable<TarefasViewModel> Tarefas { get; set; }
        public IEnumerable<RestricaoViewModel> Restricoes { get; set; }
        public IEnumerable<PlanejamentoDeAulaViewModel> PlanejamentosDeAula { get; set; }
        public IEnumerable<AtividadesViewModel> Atividades { get; set; }
    }
}