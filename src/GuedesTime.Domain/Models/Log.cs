using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuedesTime.Domain.Models
{
    public class Log : Entity
    {
        public DateTime DataHora { get; set; }
        public string Acao { get; set; }
        public string Usuario { get; set; }
        public string Detalhes { get; set; }

    }
}
