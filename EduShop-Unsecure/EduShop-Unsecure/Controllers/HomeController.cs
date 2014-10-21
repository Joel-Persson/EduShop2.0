using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EduShop_Database;
using EduShop_Unsecure.Models;

namespace EduShop_Unsecure.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BuyProductSmall(int id, string url)
        {
            var order = Session["Order"] as List<OrderRowModel> ?? new List<OrderRowModel>();

            var orderRow = order.FirstOrDefault(o => o.ProductId == id);

            if (orderRow != null)
            {
                order.Remove(orderRow);
                orderRow.Quantity ++;
                order.Add(orderRow);
                Session["Order"] = order;

                return Redirect(url);
            }
            order.Add(new OrderRowModel() { ProductId = id, Quantity = 1 });

            Session["Order"] = order;

            return Redirect(url);
        }

        public ActionResult RemoveProduct(int id, string url)
        {
            var order = Session["Order"] as List<OrderRowModel> ?? new List<OrderRowModel>();
            var orderRow = order.FirstOrDefault(o => o.ProductId ==id);

            if (orderRow != null && orderRow.Quantity > 1)
            {
                order.Remove(orderRow);
                orderRow.Quantity--;
                order.Add(orderRow);
                Session["Order"] = order;

                return Redirect(url);
            }

            order.Remove(orderRow);
            Session["Order"] = order;
            return Redirect(url);
        }

        [AllowAnonymous]
        public ActionResult Product(string category, string search)
        {
            if (category == null && search == null)
            {
                return View(ProductModel.ProductModelsToList());
            }
            if (search != null)
            {
                ViewBag.SearchTerm = search;
                return View(ProductModel.ProductModelsToListSearch(search));
            }
            ViewBag.Category = category;
            return View(ProductModel.ProductModelsToListCategory(category));

        }

        [AllowAnonymous]
        public ActionResult ProductPartial(int id)
        {
            var productModel = ProductModel.ConvertToProductModel(ProductModel.GetProduct(id));

            return PartialView("_ProductPartial", productModel);
        }


        [AllowAnonymous]
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

        [AllowAnonymous]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ProductInfo(ProductInfoModel review)
        {
            ReviewModel.AddReview(ReviewModel.ConvertToReview(review.ReviewModel));
            //ReviewModel.AddReviewToDB(ReviewModel.ConvertToReview(review.ReviewModel));
            ProductModel.AddOrUpdateProductRating(review.ReviewModel.ProductId);
            return RedirectToAction("ProductInfo", "Home", new { id = review.ReviewModel.ProductId });
        }

        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult Sidebar()
        {
            return PartialView("_Sidebar");
        }

        [AllowAnonymous]
        [ChildActionOnly]
        [HttpPost]
        public ActionResult Sidebar(string category)
        {
            return PartialView("_Sidebar");
        }

        [AllowAnonymous]
        public ActionResult Comment(int id)
        {
            return PartialView("_Comment", ReviewModel.ReviewModelsToList(id));
        }

        [AllowAnonymous]
        public ActionResult Search(SearchModel searchModel)
        {

            return PartialView("_Search", searchModel);
        }


    }
}