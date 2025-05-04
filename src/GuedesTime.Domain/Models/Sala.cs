using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuedesTime.Domain.Models
{
    public class Sala : Entity
    {
        public string Nome { get; set; }
        public int Capacidade { get; set; }
        public Guid InstituicaoId { get; set; }

        public Instituicao Instituicao { get; set; }
        public ICollection<PlanejamentoDeAula> PlanejamentosDeAula { get; set; }
    }
}
