using MwangiFinancial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MwangiFinancial.ViewModels
{
    public class DashboardViewModel
    {
        //the household the dashboard references
        public Household Household = new Household();
        //needed for form submission
        public BankAccount BankAccount = new BankAccount();
        public Budget budget = new Budget();
        public BudgetItem BudgetItem = new BudgetItem();
    }
}