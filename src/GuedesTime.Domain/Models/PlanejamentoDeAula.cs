using GuedesTime.Domain.Models.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuedesTime.Domain.Models
{
    public class PlanejamentoDeAula : Entity
    {
        public string Nome { get; set; }
        public Guid InstituicaoId { get; set; }
        public Instituicao Instituicao { get; set; }

        public ICollection<PlanejamentoDeAulaItem> Itens { get; set; }
        public ICollection<HistoricoExportacao> Historicos { get; set; }
    }
}
