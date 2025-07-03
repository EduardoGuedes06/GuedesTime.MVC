using GuedesTime.Domain.Models.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuedesTime.Domain.Models
{
    public class ConfiguracoesGenericas : Entity
    {
        public string NomeConfiguracao { get; set; }
        public string Valor { get; set; }
    }
}
