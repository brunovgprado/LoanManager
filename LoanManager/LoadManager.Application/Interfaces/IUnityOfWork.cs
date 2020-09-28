
using LoanManager.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoadManager.Application.Interfaces
{
    public interface IUnityOfWork
    {
        IGameRepository Games { get; }
        IFriendRepository Friends { get; }
        ILoanRepository Loans { get; }
    }
}
