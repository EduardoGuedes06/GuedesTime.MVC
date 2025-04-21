using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuedesTime.Domain.Models
{
    public class DisciplinaSerie : Entity
    {
        public Guid DisciplinaId { get; set; }
        public Guid SerieId { get; set; }
        public TimeSpan CargaHoraria { get; set; }

        /* EF Relations */
        public Disciplina Disciplina { get; set; }
        public Serie Serie { get; set; }
    }


}
