using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MwangiFinancial.Models
{
    public class TransactionType
    {
        public int Id { get; set; }

        public int? TransactionId { get; set; }

        public string Name { get; set; }
        public string Decsription { get; set; }

        //Virtual Navigation
        public virtual Transaction Transaction { get; set; }
    }
}