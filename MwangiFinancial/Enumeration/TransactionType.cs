using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MwangiFinancial.Enumeration
{
    public enum TransactionType
    {
        Withdrawal,
        Deposit,
        AdjustUp,
        AdjustDown,
        Payment,
        BankDraft
    }
}