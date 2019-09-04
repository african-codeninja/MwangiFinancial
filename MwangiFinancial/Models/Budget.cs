using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MwangiFinancial.Models
{
    public class Budget
    {
        //Primary Key
        public int Id { get; set; }

        //Foreign Key
        public int HouseholdId { get; set; }
        public int BudgetItemId { get; set; }
        public string BugetCategoryName { get; set; }
        public decimal TargetAmount { get; set; }

        //Virtual Nav
        public virtual Household Household { get; set; }
        public virtual ICollection<BudgetItem> BudgetItems { get; set; }

        public Budget()
        {
            BudgetItems = new HashSet<BudgetItem>();
        }
    }
}