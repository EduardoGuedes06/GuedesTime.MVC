using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuedesTime.Domain.Models
{
    public class Restricao : Entity
    {
        public string Descricao { get; set; }
        public DateTime Data { get; set; }

        /* EF Relations */
        public Professor Professor { get; set; }
    }
}
