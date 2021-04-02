using System.ComponentModel.DataAnnotations;

namespace NORTHWND.Core.BusinessModels
{
    public class CustomerRegisterModel
    {
        [Required]
        [RegularExpression(@"^[A-Z]{5}$",
            ErrorMessage = "Characters are not allowed")]
        public string CustomerId { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,20}$",
            ErrorMessage = "Characters are not allowed")]
        public string CompanyName { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,20}$",
            ErrorMessage = "Characters are not allowed")]
        public string ContactName { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$",
            ErrorMessage = "Characters are not allowed")]
        public string ContactTitle { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{2,15}$",
            ErrorMessage = "Characters are not allowed")]
        public string City { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{2,10}$",
            ErrorMessage = "Characters are not allowed")]
        public string Region { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{2,20}$",
            ErrorMessage = "Characters are not allowed")]
        public string Country { get; set; }
    }
}
