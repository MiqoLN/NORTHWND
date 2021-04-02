using System;
using System.ComponentModel.DataAnnotations;

namespace NORTHWND.Core.BusinessModels
{
    public class OrderDetailRegisterModel
    {
        [Required]
        [Range(10248,15000)]
        public int OrderId { get; set; }
        [Required]
        [Range(1,150)]
        public int ProductId { get; set; }
        [Required]
        [Range(1,100)]
        public decimal UnitPrice { get; set; }
        [Required]
        public short Quantity { get; set; }
        [Required]
        [Range(0,1)]
        public float Discount { get; set; }
    }
}
