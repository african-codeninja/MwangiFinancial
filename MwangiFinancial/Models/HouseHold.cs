using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MwangiFinancial.Models
{
    public class HouseHold
    {
        //Primary Key
        public int Id { get; set; }

        //Structure
        public string Name { get; set; }
        public string Greeting { get; set; }
        public bool IsConfigured { get; set; }
        public DateTimeOffset Created { get; set; }

        //nav
        public virtual ICollection<ApplicationUser> Memebers { get; set; }
        public virtual ICollection<BankAccount> BankAccounts { get; set; }
        public virtual ICollection<Budget> MyBudget { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Invitation> Invitations { get; set; }

        public HouseHold()
        {
            Memebers = new HashSet<ApplicationUser>();
            BankAccounts = new HashSet<BankAccount>();
            MyBudget = new HashSet<Budget>();
            Notifications = new HashSet<Notification>();
            Invitations = new HashSet<Invitation>();
        }
    }
}