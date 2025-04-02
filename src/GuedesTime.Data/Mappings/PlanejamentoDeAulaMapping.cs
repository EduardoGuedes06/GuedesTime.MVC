
using GuedesTime.Domain.Models;
using GuedesTime.Service.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuedesTime.Data.Mappings
{
    public class PlanejamentoDeAulaMapping : IEntityTypeConfiguration<PlanejamentoDeAula>
    {
        public void Configure(EntityTypeBuilder<PlanejamentoDeAula> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.Professor)
                .WithMany(prof => prof.PlanejamentosDeAula)
                .HasForeignKey(p => p.ProfessorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Disciplina)
                .WithMany(d => d.PlanejamentosDeAula)
                .HasForeignKey(p => p.DisciplinaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Sala)
                .WithMany(s => s.PlanejamentosDeAula)
                .HasForeignKey(p => p.SalaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Turma)
                .WithMany(t => t.PlanejamentosDeAula)
                .HasForeignKey(p => p.TurmaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Horario)
                .WithMany(h => h.PlanejamentosDeAula)
                .HasForeignKey(p => p.HorarioId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.HistoricoExportacao)
                .WithMany(h => h.PlanejamentosDeAula)
                .HasForeignKey(p => p.HistoricoExportacaoId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne<Instituicao>()
                .WithMany()
                .HasForeignKey("InstituicaoId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("PlanejamentoDeAulas");
        }
    }



}