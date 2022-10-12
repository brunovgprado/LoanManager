using LoanManager.Domain.Interfaces.DomainServices;
using System.Threading.Tasks;

namespace LoanManager.Domain.Interfaces.Services
{
    public interface IGenericService<TKey, T> : IBaseService<TKey, T>
    {
        Task<T> ReadAsync(TKey id);
        Task Update(T entity);
    }
}
