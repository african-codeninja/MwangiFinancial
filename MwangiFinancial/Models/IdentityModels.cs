using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MwangiFinancial.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        //Foreign Key(s)
        public int? HouseholdId { get; set; }

        //Structure
        [Required]
        [MaxLength(40, ErrorMessage = "First Name cannot be greater than 40 characters")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(40, ErrorMessage = "Last Name cannot be greater than 40 characters")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [MaxLength(40, ErrorMessage = "Display Name cannot be greater than 40 characters")]
        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }
        [Display(Name = "Avatar path")]
        public string AvatarUrl { get; set; }

        //Virtual Nav: Child of
        public virtual Household Household { get; set; }

        //Parent of
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }

        public ApplicationUser()
        {
            Transactions = new HashSet<Transaction>();
            Notifications = new HashSet<Notification>();
        }
       
        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            userIdentity.AddClaim(new Claim("HouseholdId", HouseholdId.ToString()));
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Household> Households { get; set; }
        public DbSet<Budget> MyBudget { get; set; }
        public DbSet<BudgetItem> BudgetItems { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public System.Data.Entity.DbSet<MwangiFinancial.Models.AccountType> AccountTypes { get; set; }

        public System.Data.Entity.DbSet<MwangiFinancial.Models.TransactionType> TransactionTypes { get; set; }
    }
}
