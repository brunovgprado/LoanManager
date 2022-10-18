using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoanManager.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<in TKey, T>
    {
        Task<bool> CreateAsync(T entity);
        Task<T> GetAsync(TKey id);
        Task<IEnumerable<T>> GetAsync(int offset, int limit);
        Task<bool> DeleteAsync(TKey id);
        Task<bool> UpdateAsync(T entity);
    }
}
