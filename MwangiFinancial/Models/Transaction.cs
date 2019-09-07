using MwangiFinancial.Enumeration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MwangiFinancial.Models
{
    public class Transaction
    {
        //Primary Key
        public int Id { get; set; }

        //Foreign Key
        public int? BankAccountId { get; set; }
        public int? BudgetItemId { get; set; }
        public string EnteredById { get; set; }

        //Enum Reference
        public TransactionType Type { get; set; }

        //Structure
        [Display(Name = "Low Balance Level")]
        [Range(0.0, 10000000)]
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Date { get; set; }
        
        //virtual Nav to Parents Bank Account, BudgetItem and Application User
        public virtual BankAccount BankAccount { get; set; }
        public virtual BudgetItem BudgetItem { get; set; }
        public virtual ApplicationUser EnteredBy { get; set; }

    }
}