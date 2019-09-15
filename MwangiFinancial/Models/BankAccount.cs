using MwangiFinancial.Enumeration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MwangiFinancial.Models
{
    public class BankAccount
    {
        //Primary Key
        public int Id { get; set; }

        //Foreign Keys
        public int HouseholdId { get; set; }
        public string OwnerUserId { get; set; }
        public int? AccountTypeId { get; set; }

        //Structure
        
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public State State { get; set; }
        public string Zip { get; set; }
        public double StartingBalance { get; set; }

        
        public double LowBalance { get; set; }

        
        public double CurrentBalance { get; set; }

        public DateTimeOffset Created { get; set; }

        //Nav to Parent Household
        public virtual Household Household { get; set; }

        //Parent of: Transaction and Enum Bank Account Types
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<AccountType> AccountTypes { get; set; }


        public BankAccount()
        {
            Transactions = new HashSet<Transaction>();
            AccountTypes = new HashSet<AccountType>();
        }       
    }
}