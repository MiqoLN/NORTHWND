using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NORTHWND.Core.BusinessModels
{
    public class CustomerChangeModel
    {
        [Required]
        public string CustomerId { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string City { get; set; }

        public string Region { get; set; }

        public string Country { get; set; }

    }
}
