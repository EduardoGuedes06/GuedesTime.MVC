using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuedesTime.Domain.Models
{
    public class Disciplina : Entity
    {
        public string Nome { get; set; }
        public TimeSpan CargaHoraria { get; set; }

        public Guid InstituicaoId { get; set; }
        public bool? Ativo { get; set; }
        /* EF Relations */
        public Instituicao Instituicao { get; set; }
        public ICollection<DisciplinaProfessor> DisciplinasProfessores { get; set; }
        public ICollection<PlanejamentoDeAula> PlanejamentosDeAula { get; set; }
    }
}
