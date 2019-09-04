using MwangiFinancial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MwangiFinancial.Models
{
    public class Notification
    {
        //key
        public int Id { get; set; }

        //Foreign Key
        public int HouseholdId { get; set; }
        public string CreatorId { get; set; }

        //Structure
        public DateTimeOffset Created { get; set; }
        public string Subject { get; set; }
        public string NotificationBody { get; set; }
        public bool Read { get; set; }

        //NavigationT
        public virtual Household Household { get; set; }
        public ApplicationUser Creator { get; set; }      
    }
}