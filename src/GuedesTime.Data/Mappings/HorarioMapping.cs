
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

            // Relacionamento com PlanejamentoDeAula (um para muitos)
            builder.HasMany(h => h.PlanejamentosDeAula)
                .WithOne(pa => pa.Horario)
                .HasForeignKey(pa => pa.HorarioId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Horarios");
        }
    }


}