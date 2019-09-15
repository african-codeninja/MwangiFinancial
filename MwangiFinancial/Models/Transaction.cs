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
        public int? TransactionTypeId { get; set; }
        public string EnteredById { get; set; }


        
        //Structure        
        public double Amount { get; set; }
        public string Payee { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Created { get; set; }
        public bool Reconciled { get; set; }
        public DateTimeOffset? ReconciledDate { get; set; }

        //virtual Nav to Parents Bank Account, BudgetItem, TransactionType and Application User
        public virtual BankAccount BankAccount { get; set; }
        public virtual BudgetItem BudgetItem { get; set; }
        public virtual ApplicationUser EnteredBy { get; set; }
    }
}