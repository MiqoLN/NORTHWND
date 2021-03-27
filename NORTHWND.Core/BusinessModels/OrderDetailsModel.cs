﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NORTHWND.Core.BusinessModels
{
    public class OrderDetailsModel
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public float Discount { get; set; }
    }
}
