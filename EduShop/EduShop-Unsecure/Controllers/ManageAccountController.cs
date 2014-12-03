using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.UI.WebControls;
using EduShop_Database;
using EduShop_Unsecure.Models;
using Microsoft.Owin.Security;

namespace EduShop_Unsecure.Controllers
{
    public class ManageAccountController : Controller
    {
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(UserModel model, string url)
        {
            if (ModelState.IsValid)
            {
                User user = UserModel.CheckForUser(model);
                if (user != null)
                {
                    var validateUser = PasswordHash.ValidatePassword(model.Password, user.Password);
                    if (validateUser)
                    {
                        SetAuthenticationCookie(user.Password);
                        if (user.IsAdmin)
                        {
                            SetAdminCookie();
                        }
                        return Redirect(url);
                    }
                }
            }
            ModelState.AddModelError("Valid", "Invalid Email or Password");
            return Redirect(url);
        }

        [AllowAnonymous]
        public ActionResult LoginRedirect(string url, UserModel model)
        {
            if (HttpContext.Request.Cookies.Get(FormsAuthentication.FormsCookieName) == null)
            {
                return View(model);
            }
            if (url != null)
            {
                Redirect(url);
            }
            return RedirectToAction("Index", "Home");
        }

        private void SetAdminCookie()
        {
            HttpCookie adminCookie = new HttpCookie("Admin") { HttpOnly = true };
            adminCookie.Value = "true";
            Response.Cookies.Add(adminCookie);
        }

        private void SetAuthenticationCookie(string user)
        {
            FormsAuthentication.SetAuthCookie(user, true);
        }

        [Authorize]
        public ActionResult LogOut(string url)
        {
            Session["Order"] = null;
            FormsAuthentication.SignOut();

            HttpCookie admin = new HttpCookie("Admin");
            admin.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(admin);
            return Redirect(url);
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            if (HttpContext.Request.Cookies.Get(FormsAuthentication.FormsCookieName) == null)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AllowAnonymous]
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

        public static List<string> GetErrorListFromModelState(ModelStateDictionary modelState)
        {
            var query = from state in modelState.Values
                        from error in state.Errors
                        select error.ErrorMessage;

            var errorList = query.ToList();
            return errorList;
        }

        public ActionResult ShoppingCart()
        {
            if (Session["Order"] != null)
            {
                var orderRows = Session["Order"] as List<OrderRowModel>;

                double price =
                    (from item in orderRows
                     let productModel = ProductModel.ConvertToProductModel(ProductModel.GetProduct(item.ProductId))
                     select (productModel.Price * item.Quantity)).Sum();

                ViewBag.TotalPrice = price;
            }
            return PartialView("_ShoppingCart");
        }

        [ChildActionOnly]
        [HttpPost]
        public ActionResult ShoppingCart(List<OrderRowModel> orderRows)
        {
            return PartialView("_ShoppingCart");
        }

        public ActionResult ShoppingCartItemPartial(int id, long quantity)
        {
            var productModel = ProductModel.ConvertToProductModel(ProductModel.GetProduct(id));
            ViewBag.Quantity = quantity;
            return PartialView("_ShoppingCartItemPartial", productModel);
        }

        [Authorize]
        public ActionResult Edit()
        {
            var ticket = GetAuthCookieValue();

            return View(UserModel.GetUser(ticket.Name));
        }

        private FormsAuthenticationTicket GetAuthCookieValue()
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
            return ticket;
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(UserModel model)
        {
            if (ModelState.IsValid)
            {
                User user = UserModel.CheckForUser(model);
                var validateUser = PasswordHash.ValidatePassword(model.Password, user.Password);
                if (validateUser)
                {
                    UserModel.AddUser(UserModel.ConvertToUser(model));

                    var model2 = UserModel.GetUserOnEmail(model.Email);
                    SetAuthenticationCookie(model2.Password);

                    return View(model);
                }
            }
            return View(model);

        }
    }
}