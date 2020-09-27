using LoanManager.Domain.Entities;
using LoanManager.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoanManager.Domain.Interfaces.DomainServices
{
    public interface IFriendDomainService : IService<Guid, Friend>
    {
    }
}
