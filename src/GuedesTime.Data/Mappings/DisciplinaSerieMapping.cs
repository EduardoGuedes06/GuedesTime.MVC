using GuedesTime.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuedesTime.Data.Mappings
{
    public class DisciplinaSerieMapping : IEntityTypeConfiguration<DisciplinaSerie>
    {
        public void Configure(EntityTypeBuilder<DisciplinaSerie> builder)
        {
            builder.HasKey(ds => ds.Id);

            builder.Property(ds => ds.CargaHoraria)
                .IsRequired();

            builder.HasOne(ds => ds.Disciplina)
                .WithMany(d => d.DisciplinasPorSerie)
                .HasForeignKey(ds => ds.DisciplinaId);

            builder.HasOne(ds => ds.Serie)
                .WithMany(s => s.Disciplinas)
                .HasForeignKey(ds => ds.SerieId);

			builder.Property(p => p.Ativo)
				.IsRequired()
				.HasDefaultValue(true);

			builder.Property(s => s.DataCriacao)
				.IsRequired();

			builder.Property(s => s.DataAlteracao);

			builder.ToTable("DisciplinasSeries");
        }

    }
}
