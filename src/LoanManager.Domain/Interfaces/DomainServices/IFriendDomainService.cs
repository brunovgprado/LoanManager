using LoanManager.Domain.Entities;
using System;

namespace LoanManager.Domain.Interfaces.DomainServices
{
    public interface IFriendDomainService : IGenericService<Guid, Friend>
    {
    }
}
