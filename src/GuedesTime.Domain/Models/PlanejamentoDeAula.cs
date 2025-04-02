using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuedesTime.Domain.Models
{
    public class PlanejamentoDeAula : Entity
    {
        public Guid ProfessorId { get; set; }
        public Guid DisciplinaId { get; set; }
        public Guid SalaId { get; set; }
        public Guid TurmaId { get; set; }
        public Guid HorarioId { get; set; }
        public Guid InstituicaoId { get; set; }

        /* EF Relations */
        public Professor Professor { get; set; }
        public Disciplina Disciplina { get; set; }
        public Sala Sala { get; set; }
        public Turma Turma { get; set; }
        public Horario Horario { get; set; }
        public Instituicao Instituicao { get; set; }

        /* Historico Exportacao */
        public Guid HistoricoExportacaoId { get; set; }
        public HistoricoExportacao HistoricoExportacao { get; set; }
    }

}
