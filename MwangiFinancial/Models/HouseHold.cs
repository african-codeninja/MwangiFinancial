using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MwangiFinancial.Models
{
    public class Household
    {
        //Primary Key
        public int Id { get; set; }

        //Structure
        [Required]
        [MaxLength(20, ErrorMessage = "Household Name cannot be greater than 20 characters")]
        [Display(Name = "Household Name")]
        public string Name { get; set; }
        [MaxLength(100, ErrorMessage = "Please enter a welcome message less than a 100 characters long")]
        [Display(Name = "Welcome Greeting")]
        public string Greeting { get; set; }
        public bool IsConfigured { get; set; }
        public DateTimeOffset Created { get; set; }

        //Virtual Nav, Parent of:
        public virtual ICollection<ApplicationUser> Memebers { get; set; }
        public virtual ICollection<BankAccount> BankAccounts { get; set; }
        public virtual ICollection<Budget> MyBudget { get; set; }
        public virtual ICollection<Invitation> Invitations { get; set; }

        public Household()
        {
            Memebers = new HashSet<ApplicationUser>();
            BankAccounts = new HashSet<BankAccount>();
            MyBudget = new HashSet<Budget>();
            Invitations = new HashSet<Invitation>();
        }
    }
}