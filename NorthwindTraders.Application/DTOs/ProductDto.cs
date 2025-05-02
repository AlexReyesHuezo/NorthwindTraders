using System.ComponentModel.DataAnnotations;

namespace NorthwindTraders.Application.DTOs
{
    public class ProductDto
    {
        public int ProductId { get; set; }

        [Required]
        [StringLength(40, ErrorMessage = "Product name cannot exceed 100 characters.")]
        public string ProductName { get; set; }

        public int? SupplierId { get; set; }

        public int? CategoryId { get; set; }

        [StringLength(20, ErrorMessage = "Quantity per unit cannot exceed 50 characters.")]
        public string QuantityPerUnit { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Unit price must be a positive value.")]
        public decimal UnitPrice { get; set; }

        [Range(0, short.MaxValue, ErrorMessage = "Units in stock must be a non-negative value.")]
        public short UnitsInStock { get; set; }

        [Range(0, short.MaxValue, ErrorMessage = "Units on order must be a non-negative value.")]
        public short UnitsOnOrder { get; set; }

        [Range(0, short.MaxValue, ErrorMessage = "Reorder level must be a non-negative value.")]
        public short ReorderLevel { get; set; }

        public bool Discontinued { get; set; }

        public string ProductNameWithCategory() => $"{ProductName} ({CategoryId})";
    }
}
