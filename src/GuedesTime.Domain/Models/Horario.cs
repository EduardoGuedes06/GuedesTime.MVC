using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuedesTime.Domain.Models
{
    public class Horario : Entity
    {
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }

        /* EF Relations */
        public ICollection<PlanejamentoDeAulaItem> PlanejamentosDeAulaItens { get; set; }

    }
}
