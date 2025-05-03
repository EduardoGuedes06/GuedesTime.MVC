
using GuedesTime.Domain.Models;
using GuedesTime.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace GuedesTime.Data.Context
{
    public class MeuDbContext : DbContext
    {
        public MeuDbContext(DbContextOptions<MeuDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }
        //Tabelas GuedesTime

        public DbSet<Atividades> Atividades { get; set; }
        public DbSet<ConfiguracoesGenericas> ConfiguracoesGenericas { get; set; }
        public DbSet<Disciplina> Disciplina { get; set; }
        public DbSet<HistoricoExportacao> HistoricoExportacao { get; set; }
        public DbSet<Horario> Horario { get; set; }
        public DbSet<Serie> Log { get; set; }
        public DbSet<PlanejamentoDeAula> PlanejamentoDeAula { get; set; }
        public DbSet<Professor> Professor { get; set; }
        public DbSet<Restricao> Restricao { get; set; }
        public DbSet<Sala> Sala { get; set; }
        public DbSet<Tarefas> Tarefas { get; set; }
        public DbSet<Turma> Turma { get; set; }
        public DbSet<Instituicao> Instituicao { get; set; }
        public DbSet<Feriado> Feriado { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<DisciplinaProfessor> DisciplinaProfessor { get; set; }
        public DbSet<ProfessorDisciplinaTurma> ProfessorDisciplinaTurma { get; set; }
        public DbSet<PlanejamentoDeAulaItem> PlanejamentoDeAulaItem { get; set; }
        public DbSet<DisciplinaProfessor> Serie { get; set; }
        public DbSet<PlanejamentoDeAulaItem> DisciplinaSerie { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeuDbContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}