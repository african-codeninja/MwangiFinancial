using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MwangiFinancial.Models
{
    public class BankAccountType
    {
        //Primary key
        public int Id { get; set; }

        //Foreign Key
        public int BankAccountId { get; set; }

        //Structure
        public string TypeName { get; set; }

        //Nav
        public virtual BankAccount BankAccount { get; set; }
    }
}