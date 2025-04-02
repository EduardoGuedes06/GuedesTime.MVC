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

        public Guid ProfessorId { get; set; }
        public Guid DisciplinaId { get; set; }
        public Guid SalaId { get; set; }
        public Guid TurmaId { get; set; }
        public Guid HorarioId { get; set; }
        public Guid HistoricoExportacaoId { get; set; }

        public ProfessorViewModel Professor { get; set; }
        public DisciplinaViewModel Disciplina { get; set; }
        public SalaViewModel Sala { get; set; }
        public TurmaViewModel Turma { get; set; }
        public HorarioViewModel Horario { get; set; }
        public HistoricoExportacaoViewModel HistoricoExportacao { get; set; }
    }
}