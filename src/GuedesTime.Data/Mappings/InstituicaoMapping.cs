
using GuedesTime.Domain.Models;

namespace GuedesTime.Data.Mappings
{
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

                builder.Property(i => i.CodigoCie)
                    .IsRequired()
                    .HasMaxLength(8);

                builder.Property(i => i.Avatar)
                    .HasColumnType("LONGTEXT")
                    .IsRequired(false);

                builder.Property(i => i.Cnpj)
                    .HasMaxLength(18)
                    .IsRequired(false);

                builder.Property(p => p.Integral)
                    .IsRequired()
                    .HasDefaultValue(false);

                builder.Property(p => p.Ativo)
                    .IsRequired()
                    .HasDefaultValue(true);

                builder.HasOne(i => i.Endereco)
                    .WithOne(e => e.Instituicao)
                    .HasForeignKey<Endereco>(e => e.InstituicaoId)
                    .OnDelete(DeleteBehavior.Cascade);

                builder.HasMany(i => i.Feriados)
                    .WithOne(f => f.Instituicao)
                    .HasForeignKey(f => f.InstituicaoId)
                    .OnDelete(DeleteBehavior.Cascade);

                builder.HasMany(i => i.Professores)
                    .WithOne(p => p.Instituicao)
                    .HasForeignKey(p => p.InstituicaoId)
                    .OnDelete(DeleteBehavior.Cascade);

                builder.HasMany(i => i.Turmas)
                    .WithOne(t => t.Instituicao)
                    .HasForeignKey(t => t.InstituicaoId)
                    .OnDelete(DeleteBehavior.Cascade);

                builder.HasMany(i => i.Disciplinas)
                    .WithOne(d => d.Instituicao)
                    .HasForeignKey(d => d.InstituicaoId)
                    .OnDelete(DeleteBehavior.Cascade);

                builder.HasMany(i => i.Salas)
                    .WithOne(s => s.Instituicao)
                    .HasForeignKey(s => s.InstituicaoId)
                    .OnDelete(DeleteBehavior.Cascade);

                builder.HasMany(i => i.Horarios)
                    .WithOne(h => h.Instituicao)
                    .HasForeignKey(h => h.InstituicaoId)
                    .OnDelete(DeleteBehavior.Cascade);

                builder.HasMany(i => i.Series)
                    .WithOne(s => s.Instituicao)
                    .HasForeignKey(s => s.InstituicaoId)
                    .OnDelete(DeleteBehavior.Cascade);

                builder.Property(p => p.Ativo)
                    .IsRequired()
                    .HasDefaultValue(true);

                builder.Property(s => s.DataCriacao)
                    .IsRequired();

                builder.Property(s => s.DataAlteracao);

                builder.ToTable("Instituicoes");
            }
        }
    }



}