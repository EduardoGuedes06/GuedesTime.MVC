using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GuedesTime.MVC.ViewModels
{
    public class PlanejamentoDeAulaItemViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid PlanejamentoDeAulaId { get; set; }
        public PlanejamentoDeAulaViewModel PlanejamentoDeAula { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Professor")]
        public Guid ProfessorId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Disciplina")]
        public Guid DisciplinaId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Sala")]
        public Guid SalaId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Turma")]
        public Guid TurmaId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Horário")]
        public Guid HorarioId { get; set; }

        public ProfessorViewModel Professor { get; set; }
        public DisciplinaViewModel Disciplina { get; set; }
        public SalaViewModel Sala { get; set; }
        public TurmaViewModel Turma { get; set; }
        public HorarioViewModel Horario { get; set; }
    }

}
