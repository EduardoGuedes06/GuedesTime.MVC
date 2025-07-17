
using GuedesTime.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuedesTime.Data.Mappings
{
    public class ProfessorDisciplinaTurmaMapping : IEntityTypeConfiguration<ProfessorDisciplinaTurma>
    {
        public void Configure(EntityTypeBuilder<ProfessorDisciplinaTurma> builder)
        {
            builder.HasKey(pdt => new { pdt.ProfessorId, pdt.DisciplinaId, pdt.TurmaId });

            builder.HasOne(pdt => pdt.Professor)
                   .WithMany()
                   .HasForeignKey(pdt => pdt.ProfessorId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(pdt => pdt.Disciplina)
                   .WithMany(d => d.ProfessoresDisciplinasTurmas)
                   .HasForeignKey(pdt => pdt.DisciplinaId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();

            builder.HasOne(pdt => pdt.Turma)
                   .WithMany()
                   .HasForeignKey(pdt => pdt.TurmaId)
                   .OnDelete(DeleteBehavior.Restrict);

			builder.Property(p => p.Ativo)
				    .IsRequired()
				    .HasDefaultValue(true);

			builder.Property(s => s.DataCriacao)
				    .IsRequired();

			builder.Property(s => s.DataAlteracao);

			builder.ToTable("ProfessoresDisciplinasTurmas");
        }
    }
}