using LoanManager.Domain.Interfaces.Repositories;

namespace LoanManager.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IGameRepository Games { get; }
        IFriendRepository Friends { get; }
        ILoanRepository Loans { get; }
    }
}
