using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MwangiFinancial.Enumeration
{
    public enum AppRole
    {
        [Display(Name = "Head of House")]
        HeadOfHouse,
        Resident,
        Lobbyist,
        Admin
    }
}