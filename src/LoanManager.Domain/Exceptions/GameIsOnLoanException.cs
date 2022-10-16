using System;

namespace LoanManager.Domain.Exceptions
{
    public class GameIsOnLoanException : Exception
    {
        public GameIsOnLoanException(string message = "Game is on loan") : base(message) { }
    }
}
