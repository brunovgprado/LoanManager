using System;

namespace LoanManager.Domain
{
    public class GameIsOnLoanException : Exception
    {
        public GameIsOnLoanException(string message = "Game is on loan") : base(message) { }
    }
}
