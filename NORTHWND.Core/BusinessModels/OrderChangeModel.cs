using System;
using System.ComponentModel.DataAnnotations;

namespace NORTHWND.Core.BusinessModels
{
    public class OrderChangeModel
    {
        [Required]
        [Range(10248, 15000)]
        public int OrderId { get; set; }
        [RegularExpression(@"^[A-Z]{5}$",
ErrorMessage = "Characters are not allowed")]
        public string CustomerId { get; set; }
        [Range(1, 1000)]
        public int? EmployeeId { get; set; }
        [Range(1, 10000)]
        public decimal? Freight { get; set; }
        [RegularExpression(@"^[a-zA-Z''-'\s]{2,15}$",
            ErrorMessage = "Characters are not allowed")]
        public string ShipName { get; set; }
        [StringLength(20)]
        public string ShipAddress { get; set; }
        [RegularExpression(@"^[a-zA-Z''-'\s]{2,15}$",
            ErrorMessage = "Characters are not allowed")]
        public string ShipCity { get; set; }
        [RegularExpression(@"^[a-zA-Z''-'\s]{2,20}$",
            ErrorMessage = "Characters are not allowed")]
        public string ShipCountry { get; set; }
    }
}
