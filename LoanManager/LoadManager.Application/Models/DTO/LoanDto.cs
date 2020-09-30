﻿using LoanManager.Domain.Enums.Loan;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoanManager.Application.Models.DTO
{
    public class LoanDto
    {
        public Guid Id { get; set; }
        public Guid FriendId { get; set; }
        public Guid GameId { get; set; }
        public DateTime LoanDate { get; set; }
        public Status LoanStatus { get; set; }
    }
}