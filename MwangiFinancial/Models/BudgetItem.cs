using System;
using System.Collections.Generic;
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
        public int TransactionId { get; set; }
        public string ItemName { get; set; }

        //Nav
        public virtual Budget Budget { get; set; }
    }
}