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

            return View(model);
        }
    }
}