using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using GuedesTime.Domain.Models;
using GuedesTime.MVC.Extensions;
using Microsoft.AspNetCore.Http;

namespace GuedesTime.MVC.ViewModels
{
    public class DisciplinaViewModel
    {
        [Key]
        public Guid? Id { get; set; }
        public Guid InstituicaoId { get; set; }

        [DisplayName("Nome")]
        public string Nome { get; set; }

        public bool? Ativo { get; set; }
        public IEnumerable<PlanejamentoDeAulaItemViewModel> PlanejamentosDeAula { get; set; }
        public IEnumerable<DisciplinaProfessorViewModel> DisciplinasProfessores { get; set; }
        public IEnumerable<DisciplinaSerieViewModel> DisciplinasPorSerie { get; set; }
        public ICollection<ProfessorDisciplinaTurmaViewModel> ProfessoresDisciplinasTurmas { get; set; }
    }
}