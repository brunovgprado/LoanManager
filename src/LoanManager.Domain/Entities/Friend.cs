using System;

namespace LoanManager.Domain.Entities
{
    public class Friend
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Cpf { get; set; }
        public bool BlockListed { get; set; }
    }
}
