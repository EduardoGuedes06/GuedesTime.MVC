
using GuedesTime.Domain.Models;
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

            builder.HasOne(r => r.Professor)
                .WithMany(p => p.Restricoes)
                .HasForeignKey(r => r.ProfessorId)
                .OnDelete(DeleteBehavior.Restrict);

			builder.Property(p => p.Ativo)
				.IsRequired()
				.HasDefaultValue(true);

			builder.Property(s => s.DataCriacao)
				.IsRequired();

			builder.Property(s => s.DataAlteracao);

			builder.ToTable("Restricoes");
        }
    }

}