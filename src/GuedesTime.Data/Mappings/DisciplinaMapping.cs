
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

            builder.Property(p => p.Ativo)
                .IsRequired()
                .HasDefaultValue(true);

            builder.HasMany(d => d.DisciplinasProfessores)
                   .WithOne(dp => dp.Disciplina)
                   .HasForeignKey(dp => dp.DisciplinaId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(d => d.DisciplinasPorSerie)
                    .WithOne(dp => dp.Disciplina)
                    .HasForeignKey(dp => dp.DisciplinaId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Instituicao)
                   .WithMany(i => i.Disciplinas)
                   .HasForeignKey(p => p.InstituicaoId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Disciplinas");
        }
    }



}