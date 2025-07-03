﻿using GuedesTime.Domain.Models;
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

			builder.Property(s => s.Codigo)
				.HasMaxLength(10);

			builder.Property(p => p.Ativo)
				.IsRequired()
				.HasDefaultValue(true);

			builder.Property(s => s.DataCriacao)
				.IsRequired();

			builder.Property(s => s.DataAlteracao);

			builder.Property(s => s.TipoEnsino)
				.IsRequired()
				.HasConversion<int>();

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
