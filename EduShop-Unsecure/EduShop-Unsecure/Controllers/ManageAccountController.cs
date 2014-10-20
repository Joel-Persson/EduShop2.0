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
            if (Request.Cookies["Auth"] != null)
            {
                return PartialView("_Login", new UserModel());
            }
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
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
                        return Redirect(url);
                    }
                }
                return Redirect(url);
            }
            return Redirect(url);
        }

        private void SetAuthenticationCookie(string user)
        {
            HttpCookie authentication = new HttpCookie("Auth") { HttpOnly = true };
            authentication.Value = user;
            Response.Cookies.Add(authentication);

        }

        public ActionResult LogOut(string url)
        {
            if (Request.Cookies["Auth"] != null)
            {
                HttpCookie authentication = new HttpCookie("Auth");
                authentication.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(authentication);
                Session["Order"] = null;
            }
            return Redirect(url);
        }

        public ActionResult Register()
        {
            if (Request.Cookies["Auth"] == null)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Register(UserModel model)
        {
            //GetErrorListFromModelState(ModelState);
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


        public ActionResult Edit()
        {
            if (Request.Cookies["Auth"] != null)
            {
                return View(UserModel.GetUser(Request.Cookies["Auth"].Value));
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Edit(UserModel model)
        {
            //GetErrorListFromModelState(ModelState);
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