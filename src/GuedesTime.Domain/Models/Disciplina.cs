using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuedesTime.Domain.Models
{
    public class Disciplina : Entity
    {
        public string Nome { get; set; }
        public Guid InstituicaoId { get; set; }
        public bool? Ativo { get; set; }

        /* EF Relations */

        public Instituicao Instituicao { get; set; }
        public ICollection<DisciplinaProfessor> DisciplinasProfessores { get; set; }
        public ICollection<PlanejamentoDeAula> PlanejamentosDeAula { get; set; }
        public ICollection<DisciplinaSerie> DisciplinasPorSerie { get; set; }
        public ICollection<ProfessorDisciplinaTurma> ProfessoresDisciplinasTurmas { get; set; }

    }

}
