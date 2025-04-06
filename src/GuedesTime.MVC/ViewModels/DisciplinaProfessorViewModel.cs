using System.ComponentModel;

namespace GuedesTime.MVC.ViewModels
{
    public class DisciplinaProfessorViewModel
    {
        public Guid DisciplinaId { get; set; }

        public Guid ProfessorId { get; set; }

        [DisplayName("Observação")]
        public string Observacao { get; set; }

        public DisciplinaViewModel Disciplina { get; set; }
        public ProfessorViewModel Professor { get; set; }
    }

}
