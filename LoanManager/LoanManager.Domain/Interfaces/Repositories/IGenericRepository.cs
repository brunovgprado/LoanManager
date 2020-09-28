using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoanManager.Domain.Interfaces.Repositories
{
    public interface IGenericRepository<TKey, T> : IBasicGenericRepository<TKey, T>
    {
        Task<T> ReadAsync(TKey id);
        void Update(T entity);
    }
}
