using System;
using System.Collections.Generic;
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
        public string Code { get; set; }
        public string Body { get; set; }
        public string ReciepientEmail { get; set; }
        public string SenderEmail { get; set; }
        public DateTimeOffset Created { get; set; }

        //Virtual Navigation
        public virtual Household Household { get; set; }
    }
}