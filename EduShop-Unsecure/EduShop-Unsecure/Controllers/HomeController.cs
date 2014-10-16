using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EduShop_Database;
using EduShop_Unsecure.Models;

namespace EduShop_Unsecure.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BuyProduct(int id)
        {
            var order = Session["Order"] as List<OrderRowModel> ?? new List<OrderRowModel>();
            order.Add(new OrderRowModel() { ProductId = id, Quantity = 1 });
            Session["Order"] = order;

            return RedirectToAction("ProductInfo", new { id });
        }

        public ActionResult BuyProductSmall(int id, string url)
        {
            var order = Session["Order"] as List<OrderRowModel> ?? new List<OrderRowModel>();
            order.Add(new OrderRowModel() { ProductId = id, Quantity = 1 });
            Session["Order"] = order;

            return Redirect(url);
        }

        public ActionResult RemoveProduct(int id, string url)
        {
            var order = Session["Order"] as List<OrderRowModel> ?? new List<OrderRowModel>();
            var orderRow = order.FirstOrDefault(o => o.ProductId ==id);
            order.Remove(orderRow);
            Session["Order"] = order;
            return Redirect(url);
        }

        public ActionResult Product(string category, string search)
        {
            if (category == null && search == null)
            {
                return View(ProductModel.ProductModelsToList());
            }
            if (search != null)
            {
                return View(ProductModel.ProductModelsToListSearch(search));
            }
            ViewBag.Category = category;
            return View(ProductModel.ProductModelsToListCategory(category));

        }

        public ActionResult ProductPartial(int id)
        {
            var productModel = ProductModel.ConvertToProductModel(ProductModel.GetProduct(id));

            return PartialView("_ProductPartial", productModel);
        }


        [ValidateInput(false)]
        public ActionResult ProductInfo(int id)
        {
            var productModel = ProductModel.ConvertToProductModel(ProductModel.GetProduct(id));

            var productinfoModel = new ProductInfoModel()
            {
                ProductModel = productModel,
                ReviewModel = null
            };

            return View(productinfoModel);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ProductInfo(ProductInfoModel review)
        {
            ReviewModel.AddReview(ReviewModel.ConvertToReview(review.ReviewModel));
            ProductModel.AddOrUpdateProductRating(review.ReviewModel.ProductId);
            return RedirectToAction("ProductInfo", "Home", new { id = review.ReviewModel.ProductId });
        }

        [ChildActionOnly]
        public ActionResult Sidebar()
        {
            return PartialView("_Sidebar");
        }

        [ChildActionOnly]
        [HttpPost]
        public ActionResult Sidebar(string category)
        {
            return PartialView("_Sidebar");
        }


        public ActionResult Comment(int id)
        {
            return PartialView("_Comment", ReviewModel.ReviewModelsToList(id));
        }
        [ChildActionOnly]
        [HttpPost]
        public ActionResult Comment(string comment)
        {
            return PartialView("_Comment");
        }

        public ActionResult Review(int id)
        {
            ViewBag.ProductId = id;
            return PartialView("_Review");
        }
        public ActionResult Search(SearchModel searchModel)
        {

            return PartialView("_Search", searchModel);
        }


    }
}