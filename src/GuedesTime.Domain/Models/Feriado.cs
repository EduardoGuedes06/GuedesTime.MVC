using GuedesTime.Domain.Models.Generics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuedesTime.Domain.Models
{
    public class Feriado : Entity
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        public DateTime Data { get; set; }

        public bool Recorrente { get; set; }

        public Guid InstituicaoId { get; set; }
        public Instituicao Instituicao { get; set; }
    }


}
