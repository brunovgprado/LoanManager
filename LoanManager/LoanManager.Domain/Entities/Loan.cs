using LoanManager.Domain.Enums.Loan;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoanManager.Domain.Entities
{
    public class Loan
    {
        public Guid Id { get; set; }
        public Friend Friend { get; set; }
        public Game Game { get; set; }
        public DateTime LoanDate { get; set; }
        public Status Status { get; set; }
    }
}
