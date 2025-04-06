using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using GuedesTime.MVC.Extensions;
using Microsoft.AspNetCore.Http;

namespace GuedesTime.MVC.ViewModels
{
    public class DisciplinaViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }

        [DisplayName("Carga Horária")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int CargaHoraria { get; set; }

        public IEnumerable<PlanejamentoDeAulaItemViewModel> PlanejamentosDeAula { get; set; }
        public IEnumerable<DisciplinaProfessorViewModel> Professores { get; set; }


    }
}