using GuedesTime.Domain.Intefaces;
using GuedesTime.Domain.Models;
using GuedesTime.Domain.Notificacoes;
using FluentValidation;
using FluentValidation.Results;
using GuedesTime.Data.Context;
using Microsoft.EntityFrameworkCore;

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
            return query;
        }

        public virtual async Task<PagedResult<T>> GetPagedByInstituicaoAsync(string? search, int page, int pageSize, bool ativo = true)
        {
            IQueryable<T> query = _context.Set<T>().AsNoTracking();

            query = ApplySearch(query, search);

            return await _pagedRepository.GetPagedResultAsync(query, pageSize, page, ativo);
        }

        public virtual async Task<PagedResult<T>> GetPagedByInstituicaoAsync(Guid instituicaoId, string? search, int page, int pageSize, bool ativo = true)
        {
            IQueryable<T> query = _context.Set<T>().AsNoTracking();
            query = query.Where(e => EF.Property<Guid>(e, "InstituicaoId") == instituicaoId);
            if (typeof(T).GetProperty("Ativo") != null)
            {
                query = query.Where(e => EF.Property<bool?>(e, "Ativo") == ativo);
            }
            query = ApplySearch(query, search);
            return await _pagedRepository.GetPagedResultAsync(query, pageSize, page);
        }



        public virtual async Task<IEnumerable<T>> GetWithoutPaginationAsync(string? search, int pageSize)
        {
            IQueryable<T> query = _context.Set<T>().AsNoTracking();

            query = ApplySearch(query, search);

            var paged = await _pagedRepository.GetPagedResultAsync(query, pageSize, 1);
            return paged.Items;
        }
    }


}