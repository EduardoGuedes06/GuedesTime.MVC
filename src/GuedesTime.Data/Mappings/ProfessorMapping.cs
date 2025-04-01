
using GuedesTime.Domain.Models;
using GuedesTime.Service.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuedesTime.Data.Mappings
{
    public class ProfessorMapping : IEntityTypeConfiguration<Professor>
    {
        public void Configure(EntityTypeBuilder<Professor> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(100);  // Nome do professor não pode ser nulo e tem um tamanho máximo de 100 caracteres

            builder.Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(200);  // O email também é obrigatório

            // Relacionamento com Tarefas (um para muitos)
            builder.HasMany(p => p.Tarefas)
                .WithOne(t => t.Professor)
                .HasForeignKey(t => t.ProfessorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento com Restricoes (um para muitos)
            builder.HasMany(p => p.Restricoes)
                .WithOne(r => r.Professor)
                .HasForeignKey(t => t.ProfessorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento com PlanejamentosDeAula (um para muitos)
            builder.HasMany(p => p.PlanejamentosDeAula)
                .WithOne(pa => pa.Professor)
                .HasForeignKey(pa => pa.ProfessorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Professores");
        }
    }


}