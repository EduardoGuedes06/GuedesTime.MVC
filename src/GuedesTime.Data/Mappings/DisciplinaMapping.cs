
using GuedesTime.Domain.Models;
using GuedesTime.Service.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuedesTime.Data.Mappings
{
    public class DisciplinaMapping : IEntityTypeConfiguration<Disciplina>
    {
        public void Configure(EntityTypeBuilder<Disciplina> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Nome)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(d => d.CargaHoraria)
                .IsRequired()
                .HasColumnType("int");

            // Relacionamento com PlanejamentoDeAula (um para muitos)
            builder.HasMany(d => d.PlanejamentosDeAula)
                .WithOne(pa => pa.Disciplina)
                .HasForeignKey(pa => pa.DisciplinaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Disciplinas");
        }
    }



}