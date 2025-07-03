
using GuedesTime.Domain.Models;
using GuedesTime.Service.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuedesTime.Data.Mappings
{
    public class AtividadesMapping : IEntityTypeConfiguration<Atividades>
    {
        public void Configure(EntityTypeBuilder<Atividades> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Descricao)
                .IsRequired()
                .HasColumnType("varchar(500)");

            builder.Property(a => a.DataInicio)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(a => a.DataFim)
                .IsRequired()
                .HasColumnType("datetime");

            // Relacionamento com Professor (muitos para um)
            builder.HasOne(a => a.Professor)
                .WithMany(p => p.Atividades)
                .HasForeignKey(a => a.ProfessorId)
                .OnDelete(DeleteBehavior.Restrict);

			builder.Property(p => p.Ativo)
				.IsRequired()
				.HasDefaultValue(true);

			builder.Property(s => s.DataCriacao)
				.IsRequired();

			builder.Property(s => s.DataAlteracao);

			builder.ToTable("Atividades");
        }
    }


}