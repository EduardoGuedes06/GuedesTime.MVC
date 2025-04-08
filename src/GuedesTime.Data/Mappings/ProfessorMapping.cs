
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
                .HasMaxLength(100);

            builder.Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Telefone)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnType("varchar(20)");

            builder.Property(p => p.Ativo)
                .IsRequired()
                .HasDefaultValue(true);

            builder.HasMany(p => p.Tarefas)
                .WithOne(t => t.Professor)
                .HasForeignKey(t => t.ProfessorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Restricoes)
                .WithOne(r => r.Professor)
                .HasForeignKey(t => t.ProfessorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.DisciplinasProfessores)
                   .WithOne(dp => dp.Professor)
                   .HasForeignKey(dp => dp.ProfessorId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Instituicao)
                   .WithMany(i => i.Professores)
                   .HasForeignKey(p => p.InstituicaoId)
                   .OnDelete(DeleteBehavior.Restrict);


            builder.ToTable("Professores");
        }
    }


}