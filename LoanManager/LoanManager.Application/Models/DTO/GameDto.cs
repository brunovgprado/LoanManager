using System;
using System.Collections.Generic;
using System.Text;

namespace LoanManager.Application.Models.DTO
{
    public class GameDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public string Platform { get; set; }
        public bool OnLoan { get; set; }
    }
}
