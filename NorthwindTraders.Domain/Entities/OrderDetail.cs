using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NorthwindTraders.Domain.Entities
{
    public class OrderDetail
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Unit price must be greater than 0")]
        public decimal UnitPrice { get; set; }

        [Required]
        [Range(1, short.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public short? Quantity { get; set; }

        [Range(0, 1.0, ErrorMessage = "Discount must be between 0 and 1")]
        public float Discount { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }

        // Calculated property for total price (not stored in database)
        public decimal GetTotalPrice()
        {
            return UnitPrice * (decimal)(Quantity ?? 1) * (1 - (decimal)Discount);
        }
    }
}
