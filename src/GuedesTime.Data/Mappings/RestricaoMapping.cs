
using GuedesTime.Domain.Models;
using GuedesTime.Service.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuedesTime.Data.Mappings
{
    public class RestricaoMapping : IEntityTypeConfiguration<Restricao>
    {
        public void Configure(EntityTypeBuilder<Restricao> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Descricao)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(r => r.Data)
                .IsRequired()
                .HasColumnType("datetime");

            // Relacionamento com Professor (muitos para um)
            builder.HasOne(r => r.Professor)
                .WithMany(p => p.Restricoes)
                .HasForeignKey(r => r.ProfessorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Restricoes");
        }
    }

}