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

            builder.HasOne(p => p.PlanejamentoDeAula)
                .WithMany(p => p.Itens)
                .HasForeignKey(p => p.PlanejamentoDeAulaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Professor)
                .WithMany()
                .HasForeignKey(p => p.ProfessorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Disciplina)
                .WithMany()
                .HasForeignKey(p => p.DisciplinaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Sala)
                .WithMany()
                .HasForeignKey(p => p.SalaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Turma)
                .WithMany()
                .HasForeignKey(p => p.TurmaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Horario)
                .WithMany(h => h.PlanejamentosDeAulaItens)
                .HasForeignKey(p => p.HorarioId)
                .OnDelete(DeleteBehavior.Cascade);

			builder.Property(p => p.Ativo)
				.IsRequired()
				.HasDefaultValue(true);

			builder.Property(s => s.DataCriacao)
				.IsRequired();

			builder.Property(s => s.DataAlteracao);

			builder.ToTable("PlanejamentoDeAulaItens");
        }
    }

}
