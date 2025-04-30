using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using GuedesTime.Domain.Models;
using GuedesTime.MVC.Extensions;
using Microsoft.AspNetCore.Http;

namespace GuedesTime.MVC.ViewModels
{
    public class SerieViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }
        public Guid InstituicaoId { get; set; }

        public InstituicaoViewModel Instituicao { get; set; }
        public ICollection<TurmaViewModel> Turmas { get; set; }
        public ICollection<DisciplinaSerie> Disciplinas { get; set; }
    }
}