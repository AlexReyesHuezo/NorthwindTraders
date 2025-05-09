﻿using System.ComponentModel.DataAnnotations;

namespace NorthwindTraders.Application.DTOs
{
    public class OrderDto
    {
        public int OrderId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int? EmployeeId { get; set; }

        public DateTime OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int? ShipVia { get; set; }
        public decimal Freight { get; set; }

        [Required]
        [StringLength(60)]
        public string ShipName { get; set; }

        [Required]
        [StringLength(60)]
        public string ShipAddress { get; set; }

        [Required]
        [StringLength(15)]
        public string ShipCity { get; set; }

        [StringLength(15)]
        public string ShipRegion { get; set; }

        [Required]
        [StringLength(10)]
        public string ShipPostalCode { get; set; }

        [Required]
        [StringLength(15)]
        public string ShipCountry { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public bool IsAddressValid { get; set; }

        public List<OrderDetailDto> OrderDetail { get; set; } = new List<OrderDetailDto>();
        public decimal GetTotalPrice()
        {
            return OrderDetail.Sum(od => od.UnitPrice * od.Quantity * (1 - (decimal)od.Discount)) + Freight;
        }
        public string FormattedShipAddress
        {
            get
            {
                return $"{ShipName}, {ShipAddress}, {ShipCity}, {ShipRegion}, {ShipPostalCode}, {ShipCountry}";
            }
        }
    }
}