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

        public ActionResult BuyProduct(List<OrderRowModel> orderRows, int id)
        {
            if (orderRows == null)
            {
                orderRows = new List<OrderRowModel>();
            }

            var orderRowList = new List<OrderRowModel>();
            orderRowList = orderRows;

            var orderRow = new OrderRowModel()
            {
                ProductId = id,
                Quantity = 1
            };

            orderRowList.Add(orderRow);

            Session["Order"] = orderRowList;


            return RedirectToAction("ProductInfo", id);
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

        [HttpPost]
        public ActionResult ProductInfo(ProductInfoModel review)
        {
            ProductModel.AddOrUpdateProductRating(review.ReviewModel.ProductId);
            ReviewModel.AddReview(ReviewModel.ConvertToReview(review.ReviewModel));
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