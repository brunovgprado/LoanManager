using System;
using System.Collections.Generic;
using System.Text;

namespace LoanManager.Auth.Exceptions
{
    public class EmailAdressAlreadyRegistredException : Exception
    {
        public EmailAdressAlreadyRegistredException(
            string message = "There is already a registered user for this email address") 
            : base(message) {}
    }
}
