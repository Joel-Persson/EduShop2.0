using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using EduShop_Unsecure.Models;

namespace EduShop_Unsecure.Controllers
{
    public class CheckoutController : Controller
    {
        // GET: Checkout
        [HttpGet]
        [Authorize]
        public ActionResult Checkout()
        {
            //if (Request.Cookies["Auth"] != null)
            //{
                if (Session["Order"] != null)
                {
                    var list = Session["Order"] as List<OrderRowModel>;
                    long count = list.Sum(item => item.Quantity);
                    if (count > 0)
                    {
                        var orderRows = Session["Order"] as List<OrderRowModel>;

                        double price =
                            (from item in orderRows
                                let productModel =
                                    ProductModel.ConvertToProductModel(ProductModel.GetProduct(item.ProductId))
                                select (productModel.Price*item.Quantity)).Sum();

                        ViewBag.TotalPrice = price;

                        var ticket = GetAuthCookieValue();

                        var order = UserModel.ConvertToOrderModel(UserModel.GetUser(ticket.Name));
                    return View(order);
                    }
                    return RedirectToAction("Index", "Home");
                }
            //}
            return RedirectToAction("Index", "Home");
        }

        private FormsAuthenticationTicket GetAuthCookieValue()
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
            return ticket;
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout(OrderModel model)
        {
            if(Session["order"] != null)
            {
            OrderModel.AddOrder(OrderModel.ConvertToOrder(model));
            var order = OrderModel.GetLastOrder();
            foreach (var item in Session["order"] as List<OrderRowModel>)
            {
                item.OrderId = order.Id;
                OrderRowModel.AddOrderRow(OrderRowModel.ConvertToOrderRow(item));
            }
            Session["order"] = null;
            return RedirectToAction("Receipt");
            }
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult Receipt()
        {
            return View();
        }
    }
}