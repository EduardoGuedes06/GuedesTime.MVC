﻿
using GuedesTime.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuedesTime.Data.Mappings
{
    public class HistoricoExportacaoMapping : IEntityTypeConfiguration<HistoricoExportacao>
    {
        public void Configure(EntityTypeBuilder<HistoricoExportacao> builder)
        {
            builder.HasKey(h => h.Id);

            builder.Property(h => h.DataExportacao)
                .IsRequired();

            builder.Property(h => h.NomeArquivo)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(h => h.UsuarioResponsavel)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(h => h.Observacoes)
                .HasColumnType("varchar(500)");

			builder.Property(p => p.Ativo)
				.IsRequired()
				.HasDefaultValue(true);

			builder.Property(s => s.DataCriacao)
				.IsRequired();

			builder.Property(s => s.DataAlteracao);

			builder.ToTable("HistoricoExportacao");
        }
    }

}