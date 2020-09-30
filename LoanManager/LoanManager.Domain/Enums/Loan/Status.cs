using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LoanManager.Domain.Enums.Loan
{
    public enum Status : byte
    {
        [Description("Loan in progress")]
        InPogress = 1,

        [Description("Loan closed")]
        Closed = 2
    }
}
