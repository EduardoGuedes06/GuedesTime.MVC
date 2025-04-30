using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GuedesTime.MVC.ViewModels
{
    public class DisciplinaProfessorViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid DisciplinaId { get; set; }
        public Guid ProfessorId { get; set; }
        [DisplayName("Observação")]
        public string Observacao { get; set; }
        public DisciplinaViewModel Disciplina { get; set; }
        public ProfessorViewModel Professor { get; set; }
    }

}
