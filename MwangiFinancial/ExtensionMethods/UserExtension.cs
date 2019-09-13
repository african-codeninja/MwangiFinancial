using MwangiFinancial.Helpers;
using MwangiFinancial.Enumeration;
using MwangiFinancial.Models;
using System.Linq;
using System.Web;

namespace MwangiFinancial.ExtensionMethods
{
    public static class UserExtension
    {
        public static RoleHelper roleHelper = new RoleHelper();

        public static bool IsAdmin(this ApplicationUser user)
        {
            return HttpContext.Current.User.IsInRole(AppRole.Admin.ToString());
        }

        public static string GetRole(this ApplicationUser user)
        {
            return roleHelper.ListUserRoles(user.Id).FirstOrDefault();
        }

        public static bool OccupiesRole(this ApplicationUser user, string role)
        {
            return roleHelper.IsUserInRole(user.Id, role);
        }
    }
}