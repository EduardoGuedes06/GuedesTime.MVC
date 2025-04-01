
using GuedesTime.Domain.Models;
using GuedesTime.Service.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuedesTime.Data.Mappings
{
    public class ConfiguracoesGenericasMapping : IEntityTypeConfiguration<ConfiguracoesGenericas>
    {
        public void Configure(EntityTypeBuilder<ConfiguracoesGenericas> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.NomeConfiguracao)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.Valor)
                .IsRequired()
                .HasMaxLength(1000);

            builder.ToTable("ConfiguracoesGenericas");
        }
    }


}