using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EduShop_Database;
using EduShop_Unsecure.Models;

namespace EduShop_Unsecure.Controllers
{
    public class ManageAccountController : Controller
    {
        // GET: ManageAccount
        [ChildActionOnly]
        public ActionResult Login()
        {
            return PartialView("_Login", new UserModel());
        }

        [ChildActionOnly]
        [HttpPost]
        public ActionResult Login(UserModel model)
        {
            //var userModel = new UserModel();//todo....
            //var user = new User();

            //user = UserModel.CheckForUser(model);

            //userModel = UserModel.ConvertToUserModel(user);

            return PartialView("_Login");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserModel model)
        {
            if (ModelState.IsValid)
            {
                
           
            var user = new User();

            user = UserModel.ConvertToUser(model);

            bool validate = UserModel.CheckIfUSerEmailIsUnique(user);

            if (validate)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.AlertMessage = "This email is already in use";
            return View(model);
            }
            return View(model);
           
        }

        public ActionResult ShoppingCart()
        {
            return PartialView("_ShoppingCart");
        }

        [ChildActionOnly]
        [HttpPost]
        public ActionResult ShoppingCart(List<OrderRowModel> orderRows)
        {
            return PartialView("_ShoppingCart");
        }
    }
}