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
            roleHelper.RemoveUserFromRole(userId, "Member");
            roleHelper.AddUserToRole(userId, "LobbyMember");
            db.SaveChanges();
        }

        public void PromoteToHOH(string userId, string targetUserId)
        {
            roleHelper.RemoveUserFromRole(userId, "HeadofHouse" );
            roleHelper.AddUserToRole(userId, "Member");
            roleHelper.RemoveUserFromRole(targetUserId, "Member");
            roleHelper.AddUserToRole(targetUserId, "HeadofHouse");
            db.SaveChanges();
        }
    }
}