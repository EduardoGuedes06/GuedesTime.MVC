using GuedesTime.Domain.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GuedesTime.MVC.ViewModels
{
    public class DisciplinaSerieViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid DisciplinaId { get; set; }
        public Guid SerieId { get; set; }

        [DisplayName("Carga Horária")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public TimeSpan CargaHoraria { get; set; }

        public DisciplinaViewModel Disciplina { get; set; }
        public SerieViewModel Serie { get; set; }
    }

}
