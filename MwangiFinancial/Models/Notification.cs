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

        //Foreign Keys
        public int TransactionId { get; set; }
        public string ReceipientId { get; set; }

        //Structure
        public DateTimeOffset Created { get; set; }
        public string Subject { get; set; }
        public string NotificationBody { get; set; }
        public bool Read { get; set; }

        //Navigation
        public virtual HouseHold HouseHold { get; set; }
        public ApplicationUser Receipient { get; set; }
    }
}