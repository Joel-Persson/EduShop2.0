using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using EduShop_Database;

namespace EduShop_Unsecure.Models
{
    public class OrderRowModel
    {
        private static readonly EduShopEntities context = new EduShopEntities();
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public long Quantity { get; set; }

        public static OrderRow ConvertToOrderRow(OrderRowModel orderRowModel)
        {
            var orderRow = new OrderRow()
            {
                Id = orderRowModel.Id,
                OrderId = orderRowModel.OrderId,
                ProductId = orderRowModel.ProductId,
                Quantity = orderRowModel.Quantity
            };
            return orderRow;
        }

        public static OrderRowModel ConvertToOrderRowModel(OrderRow orderRow)
        {
            var orderRowModel = new OrderRowModel()
            {
                Id = orderRow.Id,
                OrderId = orderRow.OrderId,
                ProductId = orderRow.ProductId,
                Quantity = orderRow.Quantity
            };
            return orderRowModel;
        }


        public static int AddOrderRow(OrderRow orderRow)
        {
            context.OrderRowSet.AddOrUpdate(orderRow);
            return context.SaveChanges();
        }

    }
}