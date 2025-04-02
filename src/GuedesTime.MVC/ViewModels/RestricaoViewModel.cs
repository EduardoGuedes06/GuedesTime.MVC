using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using GuedesTime.MVC.Extensions;
using Microsoft.AspNetCore.Http;

namespace GuedesTime.MVC.ViewModels
{
    public class RestricaoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Descricao { get; set; }

        [DisplayName("Data")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime Data { get; set; }

        public Guid ProfessorId { get; set; }
        public ProfessorViewModel Professor { get; set; }
    }
}