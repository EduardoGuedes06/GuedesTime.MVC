using FluentValidation;
using FluentValidation.Results;
using GuedesTime.Data.Context;
using GuedesTime.Domain.Enums;
using GuedesTime.Domain.Intefaces;
using GuedesTime.Domain.Models.Generics;
using GuedesTime.Domain.Models.Utils;
using GuedesTime.Domain.Notificacoes;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GuedesTime.Service.Services
{
    public abstract class BaseService
    {
        private readonly INotificador _notificador;

        protected BaseService(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notificar(error.ErrorMessage);
            }
        }

        protected void Notificar(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));
        }

        protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validacao.Validate(entidade);

            if (validator.IsValid) return true;

            Notificar(validator);

            return false;
        }
    }

    public abstract class BaseService<T> : BaseService where T : Entity
    {
        protected readonly MeuDbContext _context;
        private readonly IPagedResultRepository<T> _pagedRepository;

        protected BaseService(INotificador notificador, MeuDbContext context, IPagedResultRepository<T> pagedRepository)
            : base(notificador)
        {
            _context = context;
            _pagedRepository = pagedRepository;
        }

		protected virtual IQueryable<T> ApplySearch(IQueryable<T> query, string? search)
		{
			if (string.IsNullOrWhiteSpace(search))
				return query;

			var parameter = Expression.Parameter(typeof(T), "e");
			Expression? predicate = null;
			foreach (var prop in typeof(T).GetProperties().Where(p => p.PropertyType == typeof(string)))
			{
				var propertyAccess = Expression.Property(parameter, prop);
				var searchTerm = Expression.Constant(search, typeof(string));
				var notNull = Expression.NotEqual(propertyAccess, Expression.Constant(null, typeof(string)));
				var contains = Expression.Call(propertyAccess, typeof(string).GetMethod("Contains", new[] { typeof(string) })!, searchTerm);
				var condition = Expression.AndAlso(notNull, contains);

				predicate = predicate == null ? condition : Expression.OrElse(predicate, condition);
			}
			if (predicate == null)
				return query;

			var lambda = Expression.Lambda<Func<T, bool>>(predicate, parameter);
			return query.Where(lambda);
		}

		public virtual async Task<PagedResult<T>> GetPagedByInstituicaoAsync(string? search, int page, int pageSize, bool ativo = true)
        {
            IQueryable<T> query = _context.Set<T>().AsNoTracking();

            query = ApplySearch(query, search);

            return await _pagedRepository.GetPagedResultAsync(query, pageSize, page, ativo);
        }

		public virtual async Task<PagedResult<T>> GetPagedByInstituicaoAsync(
			Guid instituicaoId,
			string? search,
			int page,
			int pageSize,
			bool ativo = true,
			Expression<Func<T, bool>>? filtroAdicional = null,
			Func<IQueryable<T>, IOrderedQueryable<T>>? ordenacao = null,
			params Expression<Func<T, object>>[]? includes)
		{
			IQueryable<T> query = _context.Set<T>().AsNoTracking();

			if (typeof(T).GetProperty("InstituicaoId") != null)
			{
				query = query.Where(e => EF.Property<Guid>(e, "InstituicaoId") == instituicaoId);
			}

			if (typeof(T).GetProperty("Ativo") != null)
			{
				query = query.Where(e => EF.Property<bool?>(e, "Ativo") == ativo);
			}

			if (filtroAdicional != null)
			{
				query = query.Where(filtroAdicional);
			}

			query = ApplySearch(query, search);

			if (ordenacao != null)
			{
				query = ordenacao(query);
			}

			if (includes != null)
			{
				foreach (var include in includes)
				{
					query = query.Include(include);
				}
			}

			return await _pagedRepository.GetPagedResultAsync(query, pageSize, page);
		}


		public virtual async Task<IEnumerable<T>> GetWithoutPaginationAsync(string? search, int pageSize)
        {
            IQueryable<T> query = _context.Set<T>().AsNoTracking();

            query = ApplySearch(query, search);

            var paged = await _pagedRepository.GetPagedResultAsync(query, pageSize, 1);
            return paged.Items;
        }

		#region Utils
		
		#endregion
	}


}