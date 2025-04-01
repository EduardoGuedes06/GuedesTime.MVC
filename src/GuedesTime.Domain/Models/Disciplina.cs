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
        public int CargaHoraria { get; set; }

        /* EF Relations */
        public IEnumerable<PlanejamentoDeAula> PlanejamentosDeAula { get; set; }
    }
}
