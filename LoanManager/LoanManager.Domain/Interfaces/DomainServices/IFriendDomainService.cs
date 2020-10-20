using LoanManager.Domain.Entities;
using LoanManager.Domain.Interfaces.Services;
using System;

namespace LoanManager.Domain.Interfaces.DomainServices
{
    public interface IFriendDomainService : IGenericService<Guid, Friend>
    {
    }
}
