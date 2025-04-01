
using GuedesTime.Domain.Models;
using GuedesTime.Service.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuedesTime.Data.Mappings
{
    public class TarefasMapping : IEntityTypeConfiguration<Tarefas>
    {
        public void Configure(EntityTypeBuilder<Tarefas> builder)
        {
            builder.HasKey(t => t.Id);

            // Relacionamento com Professor (muitos para um)
            builder.HasOne(t => t.Professor)
                .WithMany(p => p.Tarefas)  // Assumindo que Professor tem uma coleção de Tarefas
                .HasForeignKey(t => t.ProfessorId)
                .OnDelete(DeleteBehavior.Restrict);  // Evitar exclusão em cascata

            builder.Property(t => t.Descricao)
                .IsRequired()
                .HasMaxLength(500);  // Por exemplo, limitando a descrição a 500 caracteres

            builder.Property(t => t.DataLimite)
                .IsRequired();  // O DataLimite não pode ser nulo

            builder.ToTable("Tarefas");
        }
    }


}