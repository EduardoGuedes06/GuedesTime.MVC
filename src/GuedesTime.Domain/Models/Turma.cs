using GuedesTime.Domain.Models.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuedesTime.Domain.Models
{
    public class Turma : Entity
    {
        public string Nome { get; set; }
        public int Ano { get; set; }

        public Guid InstituicaoId { get; set; }
        public Guid SerieId { get; set; }

        public Instituicao Instituicao { get; set; }
        public Serie Serie { get; set; }

        public ICollection<PlanejamentoDeAula> PlanejamentosDeAula { get; set; }
        public ICollection<ProfessorDisciplinaTurma> ProfessoresDisciplinasTurmas { get; set; }

    }

}
