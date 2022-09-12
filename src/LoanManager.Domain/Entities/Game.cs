using System;

namespace LoanManager.Domain.Entities
{
    public class Game
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public string Platform { get; set; }
        public bool OnLoan { get; set; }
    }
}
