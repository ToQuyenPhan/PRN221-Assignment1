﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanThiToQuyenWPF.ViewModels
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public decimal? Total { get; set; }
        public string? OrderStatus { get; set; }
    }
}
