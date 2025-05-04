
using GuedesTime.Domain.Models;
using GuedesTime.Service.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuedesTime.Data.Mappings
{
    public class FeriadoMapping : IEntityTypeConfiguration<Feriado>
    {
        public void Configure(EntityTypeBuilder<Feriado> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(f => f.Data)
                .IsRequired();

            builder.Property(f => f.Recorrente)
                .IsRequired();

            builder.HasOne(f => f.Instituicao)
                .WithMany()
                .HasForeignKey(f => f.InstituicaoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Feriados");
        }
    }

}