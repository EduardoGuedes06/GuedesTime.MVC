using GuedesTime.Domain.Models.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuedesTime.Domain.Models
{
    public class DisciplinaProfessor : Entity
    {
        public Guid DisciplinaId { get; set; }
        public Disciplina Disciplina { get; set; }

        public Guid ProfessorId { get; set; }
        public Professor Professor { get; set; }

        public string Observacao { get; set; }
    }
}
