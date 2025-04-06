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
    public class PlanejamentoDeAulaItemMapping : IEntityTypeConfiguration<PlanejamentoDeAulaItem>
    {
        public void Configure(EntityTypeBuilder<PlanejamentoDeAulaItem> builder)
        {
            builder.HasKey(i => i.Id);

            builder.HasOne(i => i.PlanejamentoDeAula)
                .WithMany(p => p.Itens)
                .HasForeignKey(i => i.PlanejamentoDeAulaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(i => i.Professor)
                .WithMany()
                .HasForeignKey(i => i.ProfessorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.Disciplina)
                .WithMany()
                .HasForeignKey(i => i.DisciplinaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.Sala)
                .WithMany()
                .HasForeignKey(i => i.SalaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.Turma)
                .WithMany()
                .HasForeignKey(i => i.TurmaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.Horario)
                .WithMany()
                .HasForeignKey(i => i.HorarioId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("PlanejamentoDeAulaItens");
        }
    }
}
