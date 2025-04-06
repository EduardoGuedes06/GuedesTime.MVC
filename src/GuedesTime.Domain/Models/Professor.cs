using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuedesTime.Domain.Models
{
    public class Professor : Entity
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public Guid InstituicaoId { get; set; }

        /* EF Relations */
        public Instituicao Instituicao { get; set; }
        public IEnumerable<DisciplinaProfessor> DisciplinasProfessores { get; set; }
        public IEnumerable<Tarefas> Tarefas { get; set; }
        public IEnumerable<Restricao> Restricoes { get; set; }
        public IEnumerable<PlanejamentoDeAula> PlanejamentosDeAula { get; set; }
        public IEnumerable<Atividades> Atividades { get; set; }
    }
}
