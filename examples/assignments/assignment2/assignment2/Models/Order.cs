using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace assignment2.Models
{
    public partial class Order
    {
        public Order() => OrderDetails = new HashSet<OrderDetail>();

        public int OrderId { get; set; }
        public string CustomerId { get; set; }
        public int? EmployeeId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Ordered")]
        [DisplayFormat(DataFormatString = "{0:yyyy'-'MM'-'dd}")]
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Shipped")]
        [DisplayFormat(DataFormatString = "{0:yyyy'-'MM'-'dd}", NullDisplayText = "Not provided")]
        public DateTime? ShippedDate { get; set; }
        public int? ShipVia { get; set; }
        public decimal? Freight { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipRegion { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }

        [Display(Name = "Customer")]
        public virtual Customer Customer { get; set; }
        public virtual Employee Employee { get; set; }

        [Display(Name = "Shipper")]
        public virtual Shipper ShipViaNavigation { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
