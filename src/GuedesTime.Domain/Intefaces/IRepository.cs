using GuedesTime.Domain.Models.Generics;
using System.Linq.Expressions;

namespace GuedesTime.Domain.Intefaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task Adicionar(TEntity entity);
        Task AdicionarLista(IEnumerable<TEntity> entities);
		Task<TEntity> ObterPorId(Guid id);
        Task<List<TEntity>> ObterTodos();
        Task Atualizar(TEntity entity);
        Task Remover(Guid id);
        Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate);
        Task<int> SaveChanges();
    }
}