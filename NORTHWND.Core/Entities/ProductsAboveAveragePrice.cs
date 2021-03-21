using System;
using System.Collections.Generic;

#nullable disable

namespace NORTHWND.Core.Entities
{
    public partial class ProductsAboveAveragePrice
    {
        public string ProductName { get; set; }
        public decimal? UnitPrice { get; set; }
    }
}
