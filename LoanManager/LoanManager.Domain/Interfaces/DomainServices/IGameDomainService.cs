using LoanManager.Domain.Entities;
using LoanManager.Domain.Interfaces.Services;
using System;

namespace LoanManager.Domain.Interfaces.DomainServices
{
    public interface IGameDomainService : IGenericService<Guid, Game>
    {
    }
}
