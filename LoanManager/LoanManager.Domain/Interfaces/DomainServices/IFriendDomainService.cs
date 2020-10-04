using LoanManager.Domain.Entities;
using LoanManager.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LoanManager.Domain.Interfaces.DomainServices
{
    public interface IFriendDomainService : IGenericService<Guid, Friend>
    {
    }
}
