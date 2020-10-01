using LoanManager.Domain.Entities;
using System;

namespace LoanManager.Application.Models.DTO
{
    public class LoanDto
    {
        public Guid Id { get; set; }
        public Guid FriendId { get; set; }
        public Guid GameId { get; set; }
        public DateTime LoanDate { get; set; }
        public bool Returned { get; set; }
        public FriendDto Friend { get; set; }
        public Game Game { get; set; }

    }
}
