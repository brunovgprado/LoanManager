using LoanManager.Domain.Entities;
using System;

namespace LoanManager.Domain.Interfaces.DomainServices
{
    public interface IGameDomainService : IGenericService<Guid, Game>
    {
    }
}
