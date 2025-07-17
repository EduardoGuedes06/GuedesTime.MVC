
using GuedesTime.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuedesTime.Data.Mappings
{
    public class TarefasMapping : IEntityTypeConfiguration<Tarefas>
    {
        public void Configure(EntityTypeBuilder<Tarefas> builder)
        {
            builder.HasKey(t => t.Id);

            builder.HasOne(t => t.Professor)
                .WithMany(p => p.Tarefas)
                .HasForeignKey(t => t.ProfessorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(t => t.Descricao)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(t => t.DataLimite)
                .IsRequired();

			builder.Property(p => p.Ativo)
				.IsRequired()
				.HasDefaultValue(true);

			builder.Property(s => s.DataCriacao)
				.IsRequired();

			builder.Property(s => s.DataAlteracao);

			builder.ToTable("Tarefas");
        }
    }


}