using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NorthwindTraders.Domain.Entities
{
    public class OrderDetail
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public short? Quantity { get; set; }
        public float Discount { get; set; }

        // Navigation properties
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }

        // Calculated property for total price (not stored in database)
        public decimal GetTotalPrice()
        {
            return UnitPrice * (decimal)(Quantity ?? 0) * (1 - (decimal)Discount);
        }
    }
}
