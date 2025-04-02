using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using GuedesTime.MVC.Extensions;
using Microsoft.AspNetCore.Http;

namespace GuedesTime.MVC.ViewModels
{
    public class TarefasViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(255, ErrorMessage = "O campo {0} precisa ter no máximo {1} caracteres")]
        public string Descricao { get; set; }

        [DisplayName("Data Limite")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime DataLimite { get; set; }

        public Guid ProfessorId { get; set; }
        public ProfessorViewModel Professor { get; set; }
    }
}