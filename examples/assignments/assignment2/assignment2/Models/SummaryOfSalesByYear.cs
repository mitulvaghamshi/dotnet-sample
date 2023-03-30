using System;
using System.Collections.Generic;

#nullable disable

namespace assignment2.Models
{
    public partial class SummaryOfSalesByYear
    {
        public DateTime? ShippedDate { get; set; }
        public int OrderId { get; set; }
        public decimal? SubTotal { get; set; }
    }
}
