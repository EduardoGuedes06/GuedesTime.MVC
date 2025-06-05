using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuedesTime.Domain.Models.Utils
{
    public class DadosAgregadosInstituicao
    {
        public int TotalProfessores { get; set; }
        public int TotalTurmas { get; set; }
        public int TotalDisciplinas { get; set; }
        public int TotalSalas { get; set; }
        public int TotalHorarios { get; set; }
        public int TotalFeriados { get; set; }
        public int TotalSeries { get; set; }
    }

}
