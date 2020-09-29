using LoadManager.Domain.Interfaces;
using LoanManager.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoanManager.Infrastructure.DataAccess.Repositories
{
    public class UnitOfWork : IUnityOfWork
    {
        public UnitOfWork(
            IGameRepository gameRepository,
            IFriendRepository friendRepository,
            ILoanRepository loanRepository
            )
        {
            Games = gameRepository;
            Friends = friendRepository;
            Loans = loanRepository;
        }

        public IGameRepository Games { get; }
        public IFriendRepository Friends { get; }
        public ILoanRepository Loans { get; }
    }
}
