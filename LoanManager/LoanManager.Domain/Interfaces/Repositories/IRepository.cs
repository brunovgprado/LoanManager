using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoanManager.Domain.Interfaces.Repositories
{
    public interface IRepository<TKey, T> : IBasicRepository<TKey, T>
    {
        Task<T> ReadAsync(TKey id);
        void Update(T entity);
    }
}
