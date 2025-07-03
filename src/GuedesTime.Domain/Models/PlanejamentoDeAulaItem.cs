using GuedesTime.Domain.Models.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuedesTime.Domain.Models
{
    public class PlanejamentoDeAulaItem : Entity
    {
        public Guid PlanejamentoDeAulaId { get; set; }
        public PlanejamentoDeAula PlanejamentoDeAula { get; set; }

        public Guid ProfessorId { get; set; }
        public Professor Professor { get; set; }

        public Guid DisciplinaId { get; set; }
        public Disciplina Disciplina { get; set; }

        public Guid SalaId { get; set; }
        public Sala Sala { get; set; }

        public Guid TurmaId { get; set; }
        public Turma Turma { get; set; }

        public Guid HorarioId { get; set; }
        public Horario Horario { get; set; }

        public string Observacao { get; set; }
    }

}
