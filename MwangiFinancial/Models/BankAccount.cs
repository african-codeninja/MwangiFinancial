using System;
using System.Collections.Generic;
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
        public int BankAccountTypeId { get; set; }
        public int TransactionId { get; set; }            
        public string OwnerUserId { get; set; }

        //Structure
        public string Name { get; set; }
        public decimal StartingBalance { get; set; }
        public decimal LowBalance { get; set; }
        public decimal CurrentBalance { get; set; }

        //Nav
        public virtual Household Household { get; set; }
        public virtual BankAccountType BankAccountType { get; set; }
        public virtual Transaction Transaction { get; set; }
        public virtual ApplicationUser OwnerUser { get; set; }



        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<BankAccountType> BankAccountTypes { get; set; }
        public BankAccount()
        {
            Transactions = new HashSet<Transaction>();
            BankAccountTypes = new HashSet<BankAccountType>();
        }       
    }
}