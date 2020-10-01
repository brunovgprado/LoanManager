using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanManager.Api.Models.Request
{
    public class CreateFriendRequest
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
}
