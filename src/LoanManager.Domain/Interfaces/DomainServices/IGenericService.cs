using System.Threading.Tasks;

namespace LoanManager.Domain.Interfaces.DomainServices
{
    public interface IGenericService<TKey, T> : IBaseService<TKey, T>
    {
        Task<T> GetAsync(TKey id);
        Task<bool> UpdateAsync(T entity);
    }
}
