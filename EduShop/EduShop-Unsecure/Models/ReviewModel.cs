using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Configuration;
using EduShop_Database;


namespace EduShop_Unsecure.Models
{
    public class ReviewModel
    {

        public int Id { get; set; }

        public int ProductId { get; set; }

        [Required(ErrorMessage = "Please enter title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter review")]
        public string Content { get; set; }

        public double Rating { get; set; }

        public DateTime DateAdded { get; set; }

        public static ReviewModel ConvertToReviewModel(Review review)
        {

            var reviewModel = new ReviewModel()
            {
                Id = review.Id,
                ProductId = review.ProductId,
                Content = review.Content,
                Title = review.Title,
                Rating = review.Rating,
                DateAdded = review.DateAdded
            };
            return reviewModel;
        }

        public static Review ConvertToReview(ReviewModel review)
        {
            var reviewModel = new Review()
            {
                Id = review.Id,
                ProductId = review.ProductId,
                Content = review.Content,
                Title = review.Title,
                Rating = review.Rating,
                DateAdded = review.DateAdded
            };
            return reviewModel;
        }

        public static List<Review> GetReviews(int id)
        {
            using (var _context = new EduShopEntities())
            {
                return (
                    from c in _context.ReviewSet
                    where c.ProductId == id
                    orderby c.DateAdded descending
                    select c).ToList();
            }
        }

        public static int AddReview(Review review)
        {
            using (var _context = new EduShopEntities())
            {
                _context.ReviewSet.Add(review);
                return _context.SaveChanges();
            }
        }
        public static void AddReviewToDB(Review review)
        {
                string conString = ConfigurationManager.ConnectionStrings["DatabaseModel"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText =
                            @"INSERT INTO [dbo].[ReviewSet]([Title],[Content],[Rating],[DateAdded],[ProductId]) VALUES('{0}','{1}',{2},'{3}',{4})";
                        command.CommandText = string.Format(command.CommandText, review.Title, review.Content, review.Rating, review.DateAdded, review.ProductId);
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
        }

        public static List<ReviewModel> ReviewModelsToList(int id)
        {
            var allReviewModels = new List<ReviewModel>();
            var allReviews = new List<Review>();

            allReviews = GetReviews(id);

            foreach (var item in allReviews)
            {
                allReviewModels.Add(ConvertToReviewModel(item));
            }
            return allReviewModels;
        }

        public static int RoundUpStarsToEven(double starRating)
        {
            int evenRating = Convert.ToInt32(Math.Round(starRating, MidpointRounding.ToEven));
            return evenRating;
        }

        public static int CalculateAverageRating(int id)
        {
            List<Review> reviews = new List<Review>();
            reviews = GetReviews(id);
            double value = reviews.Sum(item => item.Rating);
            return RoundUpStarsToEven(value / reviews.Count);
        }
    }


}