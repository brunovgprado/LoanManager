using System;
using System.Collections.Generic;
using System.Text;

namespace LoanManager.Domain
{
    public class GameIsOnLoanException : Exception
    {
        public GameIsOnLoanException(string message = "Game is on loan") : base(message) { }
    }
}
