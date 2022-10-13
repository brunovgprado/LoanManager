using System;

namespace LoanManager.Auth.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string message = "Credentials do not match any user account") : base(message) { }
    }
}
