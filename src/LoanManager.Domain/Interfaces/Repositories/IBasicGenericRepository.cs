using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoanManager.Domain.Interfaces.Repositories
{
    public interface IBasicGenericRepository<TKey, T>
    {
        Task<int> CreateAsync(T entity);
        Task<IEnumerable<T>> ReadAllAsync(int offset, int limit);
        Task<int> DeleteAsync(TKey id);
    }
}
