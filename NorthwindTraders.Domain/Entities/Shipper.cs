using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace NorthwindTraders.Domain.Entities
{
    public class Shipper
    {
        public int ShipperId { get; set; }

        [Required]
        [StringLength(40)]
        public string CompanyName { get; set; }

        [StringLength(24)]
        public string Phone { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
