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

        //Structure
        [Required(ErrorMessage ="Please Enter a name for your Account")]
        [Display(Name = "Account Name")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please choose an account type")]
        [Display(Name = "Account Type")]
        public BankAccountType Type { get; set; }
        [Display(Name = "Starting Balance")]
        [Range(0.0, 10000000)]
        public decimal StartingBalance { get; set; }
        [Display(Name = "Low Balance Level")]
        [Range(0.0, 10000000)]
        public decimal LowBalance { get; set; }
        [Display(Name = "Current Balance")]
        [Range(0.0, 10000000)]
        public decimal CurrentBalance { get; set; }

        //Nav to Parent Household
        public virtual Household Household { get; set; }

        //Parent of: Transaction and Enum Bank Account Types
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<BankAccountType> BankAccountTypes { get; set; }


        public BankAccount()
        {
            Transactions = new HashSet<Transaction>();
            BankAccountTypes = new HashSet<BankAccountType>();
        }       
    }
}