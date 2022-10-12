using System.Threading.Tasks;

namespace LoanManager.Domain.Interfaces.Repositories
{
    public interface IGenericRepository<TKey, T> : IBasicGenericRepository<TKey, T>
    {
        Task<T> ReadAsync(TKey id);
        Task<int> Update(T entity);
    }
}
