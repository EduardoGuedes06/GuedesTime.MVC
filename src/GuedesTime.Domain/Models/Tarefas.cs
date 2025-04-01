using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuedesTime.Domain.Models
{
    public class Tarefas : Entity
    {
        public string Descricao { get; set; }
        public DateTime DataLimite { get; set; }
        public Guid ProfessorId { get; set; }

        /* EF Relations */
        public Professor Professor { get; set; }
    }
}
