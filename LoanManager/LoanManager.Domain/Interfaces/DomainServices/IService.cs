using LoanManager.Domain.Interfaces.DomainServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LoanManager.Domain.Interfaces.Services
{
    public interface IService<TKey, T> : IBasicService<TKey, T>
    {
        Task<T> ReadAsync(TKey id);
        void Update(T entity);
    }
}
