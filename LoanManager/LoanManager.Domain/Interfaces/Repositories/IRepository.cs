using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoanManager.Domain.Interfaces.Repositories
{
    public interface IRepository<TKey, T>
    {
        Task CreateAsync(T entity);
        Task DeleteAsync(TKey id);
        Task<T> ReadAsync(TKey id);
        IEnumerable<T> ReadAll();
        Task<IEnumerable<T>> ReadAllAsync();
        void Update(T entity);
        Task<int> SaveChangesAsync();
    }
}
