using System;
using System.ComponentModel.DataAnnotations;

namespace NORTHWND.Core.BusinessModels
{
    public class EmployeeRegisterModel
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,20}$",
            ErrorMessage = "Characters are not allowed")]
        public string LastName { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,20}$",
            ErrorMessage = "Characters are not allowed")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,20}$",
            ErrorMessage = "Characters are not allowed")]
        public string Title { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,4}$",
            ErrorMessage = "Characters are not allowed")]
        public string TitleOfCourtesy { get; set; }
        [Required]
        [Range(typeof(DateTime), "1/1/1950", "1/1/2001",
        ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime? BirthDate { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,15}$",
            ErrorMessage = "Characters are not allowed")]
        public string City { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,10}$",
            ErrorMessage = "Characters are not allowed")]
        public string Region { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,20}$",
            ErrorMessage = "Characters are not allowed")]
        public string Country { get; set; }
    }
}
