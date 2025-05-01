using System.ComponentModel.DataAnnotations;

namespace NorthwindTraders.Application.DTOs
{
    public class ItemDto
    {
        public int ItemId { get; set; }

        [Required]
        [StringLength(40)]
        public string ProductName { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [Required]
        public int? CategoryId { get; set; }

        [Required]
        public int? SupplierId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Unit price must be greater than 0")]
        public decimal UnitPrice { get; set; }

        [Required]
        public short UnitsInStock { get; set; }

        [Required]
        public short UnitsOnOrder { get; set; }

        public bool Discontinued { get; set; }

        [StringLength(20)]
        public string QuantityPerUnit { get; set; }

        public decimal TotalValue => UnitPrice * UnitsInStock;
    }
}
