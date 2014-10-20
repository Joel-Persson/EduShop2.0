using System.Data.Entity.Migrations;
using EduShop_Database;

namespace EduShop_Unsecure.Models
{
    public class OrderRowModel
    {
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


        public static int AddOrderRow(OrderRow orderRow)
        {
            using (var _context = new EduShopEntities())
            {
                _context.OrderRowSet.AddOrUpdate(orderRow);
                return _context.SaveChanges();
            }
        }

    }
}