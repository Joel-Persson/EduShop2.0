using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EduShop_Database;
using EduShop_Unsecure.Models;

namespace EduShop_Unsecure.Controllers
{
    public class CheckoutController : Controller
    {
        // GET: Checkout
        [HttpGet]
        public ActionResult Checkout()
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
            var order = UserModel.ConvertToOrderModel(UserModel.GetUser(Request.Cookies["Auth"].Value));
            return View(order);
        }

        [HttpPost]
        public ActionResult Checkout(OrderModel model)
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

        public ActionResult Receipt()
        {
            return View();
        }
    }
}