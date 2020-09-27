using System;
using System.Collections.Generic;
using System.Text;

namespace LoanManager.Domain.Entities
{
    public class Friend
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
}
