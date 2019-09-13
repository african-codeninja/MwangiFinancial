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

        [Required(ErrorMessage ="Please Enter the address of you bank")]
        [Display(Name = "Bank Address1")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        public string Address1 { get; set; }

        [Display(Name = "Bank Address2")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        public string Address2 { get; set; }

        [Required]
        [Display(Name = "City")]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string City { get; set; }

        [Required(ErrorMessage = "Please choose an account type")]
        [Display(Name = "State")]
        public State State { get; set; }

        [Required(ErrorMessage = "Please choose an account type")]
        [Display(Name = "Account Type")]
        public BankAccountType Type { get; set; }

        [Required(ErrorMessage ="Please enter the Zip Code")]
        [MaxLength(5),MinLength(5)]
        [Display(Name = "Zip Code")]

        public int Zip { get; set; }

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