using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MwangiFinancial.Models
{
    public class BudgetItem
    {
        //Primary Key
        public int Id { get; set; }

        //Foreign Keys
        public int BudgetId { get; set; }

        //Structure
        public string ItemName { get; set; }
        public DateTimeOffset Created { get; set;}
        public double Target { get; set; }

        //Navigation to parent
        public virtual Budget Budget { get; set; }

        //parent of: Transaction
        public virtual ICollection<Transaction> Transactions { get; set; }

        public BudgetItem()
        {
            Transactions = new HashSet<Transaction>();
        }
    }
}