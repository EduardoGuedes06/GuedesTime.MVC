using GuedesTime.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuedesTime.Data.Mappings
{
    public class SerieMapping : IEntityTypeConfiguration<Serie>
    {
        public void Configure(EntityTypeBuilder<Serie> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(s => s.Instituicao)
                .WithMany(i => i.Series)
                .HasForeignKey(s => s.InstituicaoId);

            builder.HasMany(s => s.Turmas)
                .WithOne(t => t.Serie)
                .HasForeignKey(t => t.SerieId);

            builder.HasMany(s => s.Disciplinas)
                .WithOne(ds => ds.Serie)
                .HasForeignKey(ds => ds.SerieId);

            builder.ToTable("Series");
        }
    }
}
