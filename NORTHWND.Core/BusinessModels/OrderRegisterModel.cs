using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NORTHWND.Core.BusinessModels
{
    public class OrderRegisterModel
    {
        [Required]
        public string CustomerId { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public decimal Freight { get; set; }
        [Required]
        public string ShipName { get; set; }
        [Required]
        public string ShipAddress { get; set; }
        [Required]
        public string ShipCity { get; set; }
        [Required]
        public string ShipCountry { get; set; }
    }
}
