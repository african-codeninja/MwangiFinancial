using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace MwangiFinancial.ExtensionMethods
{
    public static class IdentityExtensions
    {
        public static int GetHouseholdId(this IIdentity identity)
        {
            var claimsIdentity = (ClaimsIdentity)identity;
            var householdClaim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "HouseholdId");

            if (householdClaim != null)
                return Convert.ToInt32(householdClaim.Value);
            else
                return -1;          
        }
    }
}