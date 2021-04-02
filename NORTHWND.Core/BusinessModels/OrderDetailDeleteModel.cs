using System;
using System.ComponentModel.DataAnnotations;

namespace NORTHWND.Core.BusinessModels
{
    public class OrderDetailDeleteModel
    {
        [Required]
        [Range(10248, 15000)]
        public int OrderId { get; set; }
        [Required]
        [Range(1, 150)]
        public int ProductId { get; set; }
    }
}
