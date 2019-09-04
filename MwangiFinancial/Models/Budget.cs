using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MwangiFinancial.Models
{
    public class Budget
    {
        public int Id { get; set; }
        public int HouseHoldId { get; set; }
        public int BudgetItemId { get; set; }
        public string BugetCategoryName { get; set; }
        public decimal TargetAmount { get; set; }

        //nav
        public virtual HouseHold HouseHold { get; set; }
        public virtual ICollection<BudgetItem> BudgetItems { get; set; }

        public Budget()
        {
            BudgetItems = new HashSet<BudgetItem>();
        }
    }
}