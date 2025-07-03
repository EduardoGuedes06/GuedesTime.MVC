
using GuedesTime.Domain.Models;
using GuedesTime.Domain.Models.Generics;
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
        public DbSet<Log> Log { get; set; }
        public DbSet<PlanejamentoDeAula> PlanejamentoDeAula { get; set; }
        public DbSet<Professor> Professor { get; set; }
        public DbSet<Restricao> Restricao { get; set; }
        public DbSet<Sala> Sala { get; set; }
        public DbSet<Tarefas> Tarefas { get; set; }
        public DbSet<Turma> Turma { get; set; }
        public DbSet<Instituicao> Instituicao { get; set; }
        public DbSet<Feriado> Feriado { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<DisciplinaProfessor> DisciplinaProfessor { get; set; }
        public DbSet<ProfessorDisciplinaTurma> ProfessorDisciplinaTurma { get; set; }
        public DbSet<PlanejamentoDeAulaItem> PlanejamentoDeAulaItem { get; set; }
        public DbSet<Serie> Serie { get; set; }
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
		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
		{
			foreach (var entry in ChangeTracker.Entries()
				.Where(e => e.State == EntityState.Added))
			{
				if (entry.Entity is Entity entityBase)
				{
					entityBase.DataCriacao = DateTime.UtcNow;
				}

				if (entry.Entity is EntityExpanded entidadeExpandida)
				{
					if (string.IsNullOrWhiteSpace(entidadeExpandida.Codigo))
					{
						var tipoEntidade = entry.Entity.GetType();
						var instituicaoId = (Guid?)entry.CurrentValues["InstituicaoId"];

						if (instituicaoId != null)
						{
							var method = typeof(DbContext).GetMethod(nameof(Set), Array.Empty<Type>())!;
							var genericMethod = method.MakeGenericMethod(tipoEntidade);

							var dbSet = (IQueryable)genericMethod.Invoke(this, null)!;

							var codigos = dbSet
								.Cast<object>()
								.Where(e =>
									EF.Property<Guid>(e, "InstituicaoId") == instituicaoId &&
									!string.IsNullOrEmpty(EF.Property<string>(e, "Codigo")))
								.Select(e => EF.Property<string>(e, "Codigo"));

							var maiorCodigo = await codigos
								.OrderByDescending(c => c)
								.FirstOrDefaultAsync();

							int novoCodigo = 1;
							if (int.TryParse(maiorCodigo, out int codigoInt))
								novoCodigo = codigoInt + 1;

							entidadeExpandida.Codigo = novoCodigo.ToString("D3");
						}
					}
				}
			}

			return await base.SaveChangesAsync(cancellationToken);
		}


	}
}