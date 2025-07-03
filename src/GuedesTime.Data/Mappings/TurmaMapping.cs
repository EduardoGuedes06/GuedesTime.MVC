
using GuedesTime.Domain.Models;
using GuedesTime.Service.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuedesTime.Data.Mappings
{
    public class TurmaMapping : IEntityTypeConfiguration<Turma>
    {
        public void Configure(EntityTypeBuilder<Turma> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Nome)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(t => t.Ano)
                .IsRequired();

			builder.Property(p => p.Ativo)
				.IsRequired()
				.HasDefaultValue(true);

			builder.Property(s => s.DataCriacao)
				.IsRequired();

			builder.Property(s => s.DataAlteracao);

			builder.HasOne(t => t.Serie)
                .WithMany(s => s.Turmas)
                .HasForeignKey(t => t.SerieId);

            builder.HasOne(t => t.Instituicao)
                .WithMany(i => i.Turmas)
                .HasForeignKey(t => t.InstituicaoId);

            builder.HasMany(t => t.ProfessoresDisciplinasTurmas)
                .WithOne(pdt => pdt.Turma)
                .HasForeignKey(pdt => pdt.TurmaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Turmas");
        }
    }

}