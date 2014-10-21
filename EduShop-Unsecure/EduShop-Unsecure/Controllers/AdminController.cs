using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EduShop_Unsecure.Models;

namespace EduShop_Unsecure.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [Authorize]
        public ActionResult UserListing()
        {
            return View(UserModel.GetAllUsers());
        }
    }
}