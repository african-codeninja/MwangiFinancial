using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MwangiFinancial.Models
{
    public class Invitation
    {
        //Primary Key
        public int Id { get; set; }

        //Foreign Key(s)
        public int HouseholdId { get; set; }

        //Structure
        public bool used { get; set; }
        public Guid Code { get; set; }
        [Required(ErrorMessage = "Please type in an invitation")]
        [Display(Name = "Invitation Message")]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        public string Body { get; set; }
        [EmailAddress]
        public string ReciepientEmail { get; set; }
        [EmailAddress]
        public string TimetoLive { get; set; }
        public DateTimeOffset Created { get; set; }

        //Virtual Navigation
        public virtual Household Household { get; set; }
    }
}