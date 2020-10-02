using System;
using System.Collections.Generic;
using System.Text;

namespace LoanManager.Auth.Models
{
    public class UserResponse
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
