
using GuedesTime.Domain.Models;
using GuedesTime.Service.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuedesTime.Data.Mappings
{
    public class PlanejamentoDeAulaMapping : IEntityTypeConfiguration<PlanejamentoDeAula>
    {
        public void Configure(EntityTypeBuilder<PlanejamentoDeAula> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasOne(p => p.Instituicao)
                .WithMany(i => i.PlanejamentosDeAula)
                .HasForeignKey(p => p.InstituicaoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Itens)
                .WithOne(i => i.PlanejamentoDeAula)
                .HasForeignKey(i => i.PlanejamentoDeAulaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Historicos)
                .WithOne(h => h.PlanejamentoDeAula)
                .HasForeignKey(h => h.PlanejamentoDeAulaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("PlanejamentosDeAula");
        }
    }
}