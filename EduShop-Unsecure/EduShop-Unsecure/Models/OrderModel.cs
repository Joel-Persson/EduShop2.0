using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using EduShop_Database;

namespace EduShop_Unsecure.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }

        public static OrderModel ConvertToOrderModel(Order order)
        {
            var orderModel = new OrderModel()
            {
                Id = order.Id,
                UserId = order.UserId,
                Firstname = order.Firstname,
                Lastname = order.Lastname,
                Address = order.Address,
                Zip = order.Zip,
                City = order.City,
                Phone = order.Phone
            };
            return orderModel;
        }

        public static Order ConvertToOrder(OrderModel orderModel)
        {
            var order = new Order()
            {
                Id = orderModel.Id,
                UserId = orderModel.UserId,
                Firstname = orderModel.Firstname,
                Lastname = orderModel.Lastname,
                Address = orderModel.Address,
                Zip = orderModel.Zip,
                City = orderModel.City,
                Phone = orderModel.Phone
            };
            return order;
        }

        public static int AddOrder(Order order)
        {
            using (var _context = new EduShopEntities())
            {
                _context.OrderSet.AddOrUpdate(order);
                return _context.SaveChanges();
            }
        }

        public static OrderModel GetLastOrder()
        {
            using (var _context = new EduShopEntities())
            {

                return ConvertToOrderModel((from c in _context.OrderSet
                    orderby c.Id descending
                    select c).FirstOrDefault());
            }
        }


    }



}