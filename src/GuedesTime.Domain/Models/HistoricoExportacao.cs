using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuedesTime.Domain.Models
{
    public class HistoricoExportacao : Entity
    {
        public DateTime DataExportacao { get; set; }
        public string NomeArquivo { get; set; }
        public string PathArquivo { get; set; }
        public string UsuarioResponsavel { get; set; }
        public string Observacoes { get; set; }

        /* EF Relations */
        public Guid PlanejamentoDeAulaId { get; set; }
        public PlanejamentoDeAula PlanejamentoDeAula { get; set; }
    }
}
