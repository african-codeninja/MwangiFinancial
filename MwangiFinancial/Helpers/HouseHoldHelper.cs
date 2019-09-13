using MwangiFinancial.Enumeration;
using MwangiFinancial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MwangiFinancial.Helpers
{
    public class HouseholdHelper
    {
        ApplicationDbContext db = new ApplicationDbContext();
        RoleHelper roleHelper = new RoleHelper();

        public void RemoveResidentFromHousehold(string userId)
        {
            var user = db.Users.Find(userId);
            user.HouseholdId = db.Households.FirstOrDefault(h => h.Name == "The Lobby").Id;
            roleHelper.RemoveUserFromRole(userId, AppRole.Resident);
            roleHelper.AddUserToRole(userId, AppRole.Lobbyist);
            db.SaveChanges();
        }

        public void PromoteToHOH(string userId, string targetUserId)
        {
            roleHelper.RemoveUserFromRole(userId, AppRole.HeadOfHouse);
            roleHelper.AddUserToRole(userId, AppRole.Resident);
            roleHelper.RemoveUserFromRole(targetUserId, AppRole.Resident);
            roleHelper.AddUserToRole(targetUserId, AppRole.HeadOfHouse);
            db.SaveChanges();
        }
    }
}