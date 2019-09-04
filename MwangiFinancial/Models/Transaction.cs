using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MwangiFinancial.Models
{
    public class Transaction
    {
        //Primary Key
        public int Id { get; set; }

        //Foreign Key
        public int BankAccountId { get; set; }
        public int? BudgetItemId { get; set; }
        public string EnteredById { get; set; }
        public int TransactionTypeId { get; set; }

        //Structure
        public string Amount { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Date { get; set; }
        
        //virtual Nav
        public virtual BankAccount BankAccount { get; set; }
        public virtual BudgetItem BudgetItem { get; set; }
        public virtual ApplicationUser EnteredBy { get; set; }
        public virtual ICollection<TransactionType> TransactionTypes { get; set; }

        public Transaction()
        {
            TransactionTypes = new HashSet<TransactionType>();
        }
    }
}