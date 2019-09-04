using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MwangiFinancial.Models
{
    public class TransactionType
    {
        //primary key
        public int Id { get; set; }

        //Foreign key
        public int TransactionId { get; set; }

        //Structure
        public string Descrition { get; set; }

        //Virtual Nav
        public virtual Transaction Transaction { get; set; }
    }
}