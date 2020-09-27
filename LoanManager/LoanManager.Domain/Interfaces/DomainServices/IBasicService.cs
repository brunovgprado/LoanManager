using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LoanManager.Domain.Interfaces.DomainServices
{
    public interface IBasicService<TKey, T>
    {
        Task CreateAsync(T entity);
        Task<IEnumerable<T>> ReadAllAsync(int offset, int limit);
        Task DeleteAsync(TKey id);
    }
}
