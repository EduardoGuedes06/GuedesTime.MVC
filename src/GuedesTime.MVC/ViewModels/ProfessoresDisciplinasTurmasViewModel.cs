using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using GuedesTime.Domain.Models;
using GuedesTime.MVC.Extensions;
using Microsoft.AspNetCore.Http;

namespace GuedesTime.MVC.ViewModels
{
    public class ProfessorDisciplinaTurmaViewModel
    {
        [Key]
        public Guid Id { get; set; }

        public Guid ProfessorId { get; set; }
        public Guid DisciplinaId { get; set; }
        public Guid TurmaId { get; set; }

        public ProfessorViewModel Professor { get; set; }
        public DisciplinaViewModel Disciplina { get; set; }
        public TurmaViewModel Turma { get; set; }
    }
}