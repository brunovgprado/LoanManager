
using LoanManager.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoadManager.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IGameRepository Games { get; }
        IFriendRepository Friends { get; }
        ILoanRepository Loans { get; }
    }
}
