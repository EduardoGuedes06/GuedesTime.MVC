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
        public Guid InstituicaoId { get; set; }

        /* EF Relations */
        public Instituicao Instituicao { get; set; }
        public ICollection<PlanejamentoDeAulaItem> PlanejamentosDeAulaItens { get; set; }

    }
}
