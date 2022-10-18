using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoanManager.Domain.Interfaces.DomainServices
{
    public interface IBaseService<TKey, T>
    {
        Task<TKey> CreateAsync(T entity);
        Task<IEnumerable<T>> GetAsync(int offset, int limit);
        Task<bool> DeleteAsync(TKey id);
    }
}
