
using GuedesTime.Domain.Models;
using GuedesTime.Service.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuedesTime.Data.Mappings
{
    public class InstituicaoMapping : IEntityTypeConfiguration<Instituicao>
    {
        public void Configure(EntityTypeBuilder<Instituicao> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Nome)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(i => i.Inep)
                .IsRequired()
                .HasMaxLength(8);

            builder.Property(i => i.Cnpj)
                .HasMaxLength(18)
                .IsRequired(false);

            builder.HasOne(i => i.Endereco)
                .WithOne(e => e.Instituicao)
                .HasForeignKey<Endereco>(e => e.InstituicaoId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasMany(i => i.Feriados)
                .WithOne()
                .HasForeignKey("InstituicaoId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(i => i.Professores)
                .WithOne()
                .HasForeignKey("InstituicaoId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(i => i.Turmas)
                .WithOne()
                .HasForeignKey("InstituicaoId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(i => i.Disciplinas)
                .WithOne()
                .HasForeignKey("InstituicaoId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(i => i.Salas)
                .WithOne()
                .HasForeignKey("InstituicaoId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(i => i.Horarios)
                .WithOne()
                .HasForeignKey("InstituicaoId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Instituicoes");
        }

    }


}