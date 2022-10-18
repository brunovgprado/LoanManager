using System;

namespace LoanManager.Api.Models.Request
{
    public class CreateLoanRequestDto
    {
        public Guid FriendId { get; set; }
        public Guid GameId { get; set; }
    }
}
