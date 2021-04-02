using System;
using System.ComponentModel.DataAnnotations;

namespace NORTHWND.Core.BusinessModels
{
    public class ProductChangeModel
    {
        [Required]
        [Range(1, 150)]
        public int ProductId { get; set; }
        [RegularExpression(@"^[a-zA-Z''-'\s]{2,10}$",
            ErrorMessage = "Characters are not allowed")]
        public string ProductName { get; set; }
        [Range(1,50)]
        public int? SupplierId { get; set; }
        [Range(1,20)]
        public int? CategoryId { get; set; }
        public string QuantityPerUnit { get; set; }
        [Range(1,100)]
        public decimal? UnitPrice { get; set; }
        [Range(1,1000)]
        public short? UnitsInStock { get; set; }

    }
}
