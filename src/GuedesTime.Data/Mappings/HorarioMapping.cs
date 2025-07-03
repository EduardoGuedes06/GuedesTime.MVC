
using GuedesTime.Domain.Models;
using GuedesTime.Service.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuedesTime.Data.Mappings
{
    public class HorarioMapping : IEntityTypeConfiguration<Horario>
    {
        public void Configure(EntityTypeBuilder<Horario> builder)
        {
            builder.HasKey(h => h.Id);

            builder.Property(h => h.Inicio)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(h => h.Fim)
                .IsRequired()
                .HasColumnType("datetime");

            builder.HasMany(h => h.PlanejamentosDeAulaItens)
                .WithOne(p => p.Horario)
                .HasForeignKey(p => p.HorarioId)
                .OnDelete(DeleteBehavior.Cascade);

			builder.Property(p => p.Ativo)
				.IsRequired()
				.HasDefaultValue(true);

			builder.Property(s => s.DataCriacao)
				.IsRequired();

			builder.Property(s => s.DataAlteracao);

			builder.ToTable("Horarios");
        }
    }


}