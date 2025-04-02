using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using GuedesTime.MVC.Extensions;
using Microsoft.AspNetCore.Http;

namespace GuedesTime.MVC.ViewModels
{
    public class PlanejamentoDeAulaViewModel
    {
        [Key]
        public Guid Id { get; set; }

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

        [DisplayName("Histórico de Exportação")]
        public Guid? HistoricoExportacaoId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Instituição")]
        public Guid InstituicaoId { get; set; }

        public ProfessorViewModel Professor { get; set; }
        public DisciplinaViewModel Disciplina { get; set; }
        public SalaViewModel Sala { get; set; }
        public TurmaViewModel Turma { get; set; }
        public HorarioViewModel Horario { get; set; }
        public HistoricoExportacaoViewModel HistoricoExportacao { get; set; }
        public InstituicaoViewModel Instituicao { get; set; }
    }

}