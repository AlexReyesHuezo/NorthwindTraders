namespace NorthwindTraders.Domain.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int? EmployeeId { get; set; }

        public DateTime OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int? ShipVia { get; set; }
        public decimal Freight { get; set; }
        public string? ShipName { get; set; }
        public string? ShipAddress { get; set; }
        public string? ShipCity { get; set; }
        public string? ShipRegion { get; set; }
        public string? ShipPostalCode { get; set; }
        public string? ShipCountry { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public bool IsAddressValid { get; set; }

        public Customer Customer { get; set; }
        public Employee Employee { get; set; }
        public virtual ICollection<OrderDetail> OrderDetail { get; set; } = new List<OrderDetail>();
        public decimal GetTotalPrice()
        {
            return OrderDetail.Sum(od => od.UnitPrice * (od.Quantity ?? 0) * (1 - (decimal)od.Discount)) + Freight;
        }
    }
}
