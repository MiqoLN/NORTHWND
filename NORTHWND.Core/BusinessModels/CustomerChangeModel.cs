using System.ComponentModel.DataAnnotations;

namespace NORTHWND.Core.BusinessModels
{
    public class CustomerChangeModel
    {
        [Required]
        [RegularExpression(@"^[A-Z]{5}$",
            ErrorMessage = "Characters are not allowed")]
        public string CustomerId { get; set; }
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,20}$",
            ErrorMessage ="Characters are not allowed")]
        public string CompanyName { get; set; }
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,20}$",
            ErrorMessage = "Characters are not allowed")]
        public string ContactName { get; set; }
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$",
            ErrorMessage = "Characters are not allowed")]
        public string ContactTitle { get; set; }
        [RegularExpression(@"^[a-zA-Z''-'\s]{2,15}$",
            ErrorMessage = "Characters are not allowed")]
        public string City { get; set; }
        [RegularExpression(@"^[a-zA-Z''-'\s]{2,10}$",
            ErrorMessage = "Characters are not allowed")]
        public string Region { get; set; }
        [RegularExpression(@"^[a-zA-Z''-'\s]{2,20}$",
            ErrorMessage = "Characters are not allowed")]
        public string Country { get; set; }

    }
}
