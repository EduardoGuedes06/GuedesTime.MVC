using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuedesTime.Domain.Models
{
    public class ProfessorDisciplinaTurma : Entity
    {
        public Guid ProfessorId { get; set; }
        public Guid DisciplinaId { get; set; }
        public Guid TurmaId { get; set; }

        public Professor Professor { get; set; }
        public Disciplina Disciplina { get; set; }
        public Turma Turma { get; set; }
    }

}
