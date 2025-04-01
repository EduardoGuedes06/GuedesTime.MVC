
using GuedesTime.Domain.Models;
using GuedesTime.Service.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuedesTime.Data.Mappings
{
    public class LogMapping : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.HasKey(l => l.Id);

            builder.Property(l => l.DataHora)
                .IsRequired();

            builder.Property(l => l.Acao)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(l => l.Usuario)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(l => l.Detalhes)
                .HasColumnType("varchar(500)");

            builder.ToTable("Logs");
        }
    }

}