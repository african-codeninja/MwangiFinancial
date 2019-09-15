using MwangiFinancial.ViewModels;
using MwangiFinancial.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MwangiFinancial.Helpers
{
    public class BadgeHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        public static int AccountsOverBudget(DashboardViewModel dbvm)
        {
            var total = dbvm.Household.BankAccounts.Where(b => b.CurrentBalance < 0).Count();
            return total;
        }

        public static int LowBalanceAccounts(DashboardViewModel dbvm)
        {
            var total = dbvm.Household.BankAccounts.Where(b => b.CurrentBalance >= 0 && b.CurrentBalance <= b.LowBalance).Count();
            return total;
        }

        public static int CategoriesOverBudget(DashboardViewModel dbvm)
        {
            var tally = 0;
            //foreach (var category in db.MyBudget.Where(i => i.HouseholdId == dbvm.Household.Id).ToList())
            //{
            //    decimal total = 0;
            //    foreach (var categoryitem in category.BudgetItems.ToList())
            //    {
            //        var aMonthAgo = DateTimeOffset.UtcNow.ToLocalTime().AddMonths(-1);
            //        foreach (var transaction in db.Transactions.Where(t => t.BudgetItemId == categoryitem.Id && t.Created > aMonthAgo).ToList())
            //        {
            //            if (transaction.TransactionType == TransactionType.AdjustDown || transaction.TransactionType == TransactionType.Withdrawal)
            //            {
            //                total += transaction.Amount;
            //            }
            //            else
            //            {
            //                total -= transaction.Amount;
            //            }
            //        }
            //    }
            //    if (total > category.TargetAmount)
            //    {
            //        tally += 1;
            //    }              
            //}
            return tally;
        }
    }
}