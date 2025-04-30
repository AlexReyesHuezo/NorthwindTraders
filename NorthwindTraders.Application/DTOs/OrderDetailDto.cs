using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NorthwindTraders.Application.DTOs
{
    public class OrderDetailDto
    {
        public int ProductId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Unit price must be greater than 0")]
        public decimal UnitPrice { get; set; }

        [Required]
        [Range(1, short.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public short Quantity { get; set; }

        [Range(0, 1.0, ErrorMessage = "Discount must be between 0 and 1")]
        public float Discount { get; set; }

        public decimal TotalPrice => UnitPrice * Quantity * (1 - (decimal)Discount);
    }
}
