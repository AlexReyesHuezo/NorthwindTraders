﻿namespace NorthwindTraders.Domain.Entities
{
    public class OrderDetail
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public short? Quantity { get; set; }
        public float Discount { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }

        public decimal GetTotalLine() => (decimal)(Quantity * UnitPrice * (1 - (decimal)Discount));
    }
}
