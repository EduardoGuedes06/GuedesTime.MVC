using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuedesTime.Domain.Models
{
    public class Turma : Entity
    {
        public string Nome { get; set; }
        public int Ano { get; set; }
    }
}
