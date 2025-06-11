using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuedesTime.Domain.Models
{
    public class Serie : Entity
    {
        public string Nome { get; set; }
        public Guid InstituicaoId { get; set; }
		public bool? Ativo { get; set; }
		public Instituicao Instituicao { get; set; }
        public ICollection<Turma> Turmas { get; set; }
        public ICollection<DisciplinaSerie> Disciplinas { get; set; }
    }

}
