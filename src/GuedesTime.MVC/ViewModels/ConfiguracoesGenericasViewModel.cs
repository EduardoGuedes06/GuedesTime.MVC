using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using GuedesTime.MVC.Extensions;
using Microsoft.AspNetCore.Http;

namespace GuedesTime.MVC.ViewModels
{
    public class ConfiguracoesGenericasViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Nome da Configuração")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string NomeConfiguracao { get; set; }

        [DisplayName("Valor")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Valor { get; set; }
    }
}