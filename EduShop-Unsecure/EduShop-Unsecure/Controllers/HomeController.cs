using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EduShop_Unsecure.Models;

namespace EduShop_Unsecure.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult Product()
        //{

        //}
        public ActionResult Product(string category, string search)
        {
            if (category == null && search==null)
            {
                return View(ProductModel.ProductModelsToList());
            }
             if (search!=null)
            {
                return View(ProductModel.ProductModelsToListSearch(search));
            }
                return View(ProductModel.ProductModelsToListCategory(category));

        }

        public ActionResult ProductPartial(int id)
        {
            var productModel = ProductModel.ConvertToProductModel(ProductModel.GetProduct(id));

            return PartialView("_ProductPartial", productModel);
        }

        //[HttpPost]
        //public ActionResult ProductPartial(string product)
        //{
        //    return PartialView("_ProductPartial");
        //}

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

        [HttpPost]
        public ActionResult Review(ReviewModel review)
        {

            ReviewModel.AddReview(ReviewModel.ConvertToReview(review));
            return RedirectToAction("ProductInfo", "Home", new {id = review.ProductId});
        }

        public ActionResult Search(SearchModel searchModel)
        {

            return PartialView("_Search", searchModel);
        }


    }
}