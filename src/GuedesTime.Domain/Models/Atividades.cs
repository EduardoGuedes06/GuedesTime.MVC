using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuedesTime.Domain.Models
{
    public class Atividades : Entity
    {
        public string Descricao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

        /* EF Relations */
        public Guid ProfessorId { get; set; }
        public Professor Professor { get; set; }
    }
}
