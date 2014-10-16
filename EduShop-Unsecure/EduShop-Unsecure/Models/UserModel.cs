using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using EduShop_Database;
using Microsoft.AspNet.Identity;

namespace EduShop_Unsecure.Models
{
    public class UserModel
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Can not be empty!")]
        [EmailAddress(ErrorMessage = "Please check your email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Can not be empty!")]
        [StringLength(32, ErrorMessage = "Check your password", MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Can not be empty!")]
        [StringLength(40, ErrorMessage = "Check your firstname", MinimumLength = 2)]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Can not be empty!")]
        [StringLength(40, ErrorMessage = "Check your lastname", MinimumLength = 2)]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Can not be empty!")]
        [StringLength(70, ErrorMessage = "Check your address", MinimumLength = 2)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Can not be empty!")]
        [DataType(DataType.PostalCode, ErrorMessage = "Check your zip")]
        public string Zip { get; set; }

        [Required(ErrorMessage = "Can not be empty!")]
        [StringLength(40, ErrorMessage = "Check your City", MinimumLength = 2)]
        public string City { get; set; }

        [Required(ErrorMessage = "Can not be empty!")]
        [StringLength(20, ErrorMessage = "Your phone number is too long")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; }

        public bool IsAdmin { get; set; }

        public static User CheckForUser(UserModel userModel)
        {
            using (var _context = new EduShopEntities())
            {

                return (
                    from c in _context.UserSet
                    where c.Email == userModel.Email
                    select c).FirstOrDefault();
            }
        }

        public static UserModel ConvertToUserModel(User user)
        {
            var userModel = new UserModel()
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Address = user.Address,
                Zip = user.Zip,
                City = user.City,
                Phone = user.Phone,
                IsAdmin = user.IsAdmin
            };
            return userModel;
        }

        public static OrderModel ConvertToOrderModel(UserModel user)
        {
            var orderModel = new OrderModel()
            {
                UserId = user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Address = user.Address,
                Zip = user.Zip,
                City = user.City,
                Phone = user.Phone,
            };
            return orderModel;
        }

        public static User ConvertToUser(UserModel userModel)
        {
            var user = new User()
            {
                Id = userModel.Id,
                Email = userModel.Email,
                Password = userModel.Password,
                Firstname = userModel.Firstname,
                Lastname = userModel.Lastname,
                Address = userModel.Address,
                Zip = userModel.Zip,
                City = userModel.City,
                Phone = userModel.Phone,
                IsAdmin = userModel.IsAdmin
            };
            return user;
        }

        public static int AddUser(User user)
        {
            using (var _context = new EduShopEntities())
            {

                user.Password = PasswordHash.CreateHash(user.Password);
                _context.UserSet.AddOrUpdate(user);
                return _context.SaveChanges();
            }
        }

        public static bool CheckIfUSerEmailIsUnique(User user)
        {
            using (var _context = new EduShopEntities())
            {

                var query = (from u in _context.UserSet
                    where u.Email == user.Email
                    select u).Count();

                if (query > 0)
                {
                    return false;
                }

                AddUser(user);

                return true;
            }
        }

        public static UserModel GetUser(string password)
        {
            using (var _context = new EduShopEntities())
            {

                return ConvertToUserModel((from c in _context.UserSet
                    where c.Password == password
                    select c).FirstOrDefault());
            }
        }
    }
}