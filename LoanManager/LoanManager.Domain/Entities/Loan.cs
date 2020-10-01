using System;

namespace LoanManager.Domain.Entities
{
    public class Loan
    {
        public Guid Id { get; set; }
        public Guid FriendId { get; set; }
        public Guid GameId { get; set; }
        public DateTime LoanDate { get; set; }
        public bool Returned { get; set; }
        public Friend Friend { get; set; }
        public Game Game { get; set; }
    }
}
