using System;
using System.Collections.Generic;
using System.Text;

namespace NORTHWND.Core.BusinessModels
{
    public class CustomerViewModel
    {
        public string CustomerId { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }

    }
}
