using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace NorthwindTraders.Domain.Entities
{
    public class Order
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
        public string? ShipName { get; set; }

        [Required]
        [StringLength(60)]
        public string? ShipAddress { get; set; }

        [Required]
        [StringLength(15)]
        public string? ShipCity { get; set; }

        [StringLength(15)]
        public string? ShipRegion { get; set; }

        [Required]
        [StringLength(10)]
        public string? ShipPostalCode { get; set; }

        [Required]
        [StringLength(15)]
        public string? ShipCountry { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ICollection<OrderDetail> OrderDetail { get; set; } = new List<OrderDetail>();
    }
}
