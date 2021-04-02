using System;
using System.ComponentModel.DataAnnotations;

namespace NORTHWND.Core.BusinessModels
{
    public class OrderRegisterModel
    {
        [Required]
        [RegularExpression(@"^[A-Z]{5}$",
            ErrorMessage = "Characters are not allowed")]
        public string CustomerId { get; set; }
        [Required]
        [Range(1,1000)]
        public int EmployeeId { get; set; }
        [Required]
        [Range(1,10000)]
        public decimal Freight { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{2,15}$",
            ErrorMessage = "Characters are not allowed")]
        public string ShipName { get; set; }
        [Required]
        [StringLength(20)]
        public string ShipAddress { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{2,15}$",
            ErrorMessage = "Characters are not allowed")]
        public string ShipCity { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{2,20}$",
            ErrorMessage = "Characters are not allowed")]
        public string ShipCountry { get; set; }
    }
}
