using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduShop_Unsecure.Models
{
    public class OrderRowModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}