using GuedesTime.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuedesTime.Data.Mappings
{
    public class DisciplinaProfessorMapping : IEntityTypeConfiguration<DisciplinaProfessor>
    {
        public void Configure(EntityTypeBuilder<DisciplinaProfessor> builder)
        {
            builder.HasKey(dp => new { dp.DisciplinaId, dp.ProfessorId });

            builder.Property(dp => dp.Observacao)
                   .HasMaxLength(500);

            builder.HasOne(dp => dp.Disciplina)
                   .WithMany(d => d.DisciplinasProfessores)
                   .HasForeignKey(dp => dp.DisciplinaId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(dp => dp.Professor)
                   .WithMany(p => p.DisciplinasProfessores)
                   .HasForeignKey(dp => dp.ProfessorId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("DisciplinasProfessores");
        }
    }
}
