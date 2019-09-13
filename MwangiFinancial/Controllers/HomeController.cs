using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MwangiFinancial.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        //This is where you direct newly created users who will then create a household
        [Authorize(Roles = "LobbyMember")]
        public ActionResult Lobby()
        {
            return View();
        }

    }
}