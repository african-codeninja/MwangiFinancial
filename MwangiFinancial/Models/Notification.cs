using MwangiFinancial.Enumeration;
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
        public string HouseholdId { get; set; }

        //Structure
        public string NotificationBody { get; set; }

        public DateTimeOffset Created { get; set; }
        public NotificationType NotificationType { get; set; }
               

        //Navigation parent Applicationuser
        public virtual Household Household { get; set; }      
    }
}