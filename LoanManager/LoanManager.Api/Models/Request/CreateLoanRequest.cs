using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanManager.Api.Models.Request
{
    public class CreateLoanRequest
    {
        public Guid FriendId { get; set; }
        public Guid GameId { get; set; }
    }
}
