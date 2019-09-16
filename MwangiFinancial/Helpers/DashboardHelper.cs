using MwangiFinancial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MwangiFinancial.Helpers
{
    public class DashboardHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        public static double TargetAmountUsedThisMonth(Budget budget)
        {
            double amountSpent = 0;
            var aMonthAgo = DateTimeOffset.UtcNow.ToLocalTime().AddMonths(-1);
            var childItemIds = "";
            foreach (var item in budget.BudgetItems)
            {
                childItemIds += (item.Id + ", ");
            }

            foreach (var transaction in db.Transactions.Where(t => childItemIds.Contains(t.BudgetItemId.ToString()) && t.Created > aMonthAgo))
            {
                amountSpent += transaction.Amount;
            }
            double amountUsed = amountSpent / budget.TargetAmount;

            if (amountUsed > 1)
                amountUsed = 1;

            return amountUsed;
        }
    }
}