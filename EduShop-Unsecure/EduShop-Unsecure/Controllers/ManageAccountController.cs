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


        [HttpPost]
        public ActionResult Login(UserModel model)
        {
            if (ModelState.IsValid)
            {
                if (UserModel.CheckForUser(model) != null)
                {
                    //SetOrderSession();
                    SetAuthenticationCookie(model);
                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home", model);
        }

        private void SetOrderSession()
        {
            HttpContext.Session["Order"] = new List<OrderRowModel>();
        }

        private void SetAuthenticationCookie(UserModel model)
        {
            HttpCookie authentication = new HttpCookie("Auth"){HttpOnly = true};
            authentication.Value = model.Email;
            Response.Cookies.Add(authentication);

        }

        public ActionResult LogOut()
        {
            if (Request.Cookies["Auth"] != null)
            {
                HttpCookie authentication = new HttpCookie("Auth");
                authentication.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(authentication);
                Session["Order"] = null;
            }
            return RedirectToAction("Index", "Home");
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

        public ActionResult ShoppingCartItemPartial(int id)
        {
            var productModel = ProductModel.ConvertToProductModel(ProductModel.GetProduct(id));

            return PartialView("_ShoppingCartItemPartial", productModel);
        }


        public ActionResult Edit()
        {
            return View(UserModel.GetUser(Request.Cookies["Auth"].Value));
        }

        [HttpPost]
        public ActionResult Edit(UserModel model)
        {
            UserModel.AddUser(UserModel.ConvertToUser(model));

            return RedirectToAction("Index", "Home");
        }
    }
}