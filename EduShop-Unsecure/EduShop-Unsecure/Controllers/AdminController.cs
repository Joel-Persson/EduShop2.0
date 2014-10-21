using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EduShop_Unsecure.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult UserListing()
        {
            return View();
        }
    }
}