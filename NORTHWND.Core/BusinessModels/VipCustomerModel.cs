using System;
using System.Collections.Generic;
using System.Text;

namespace NORTHWND.Core.BusinessModels
{
    public class VipCustomerModel
    {
        public string CustomerId { get; set; }
        public string CompanyName { get; set; }
        public decimal TotalOrderAmount { get; set; }
    }
}
