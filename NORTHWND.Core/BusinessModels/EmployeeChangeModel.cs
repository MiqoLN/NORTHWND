using System;
using System.ComponentModel.DataAnnotations;

namespace NORTHWND.Core.BusinessModels
{
    public class EmployeeChangeModel
    {
        [Required]
        public int EmployeeId { get; set; }
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,20}$",
            ErrorMessage = "Characters are not allowed")]
        public string LastName { get; set; }
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,20}$",
            ErrorMessage = "Characters are not allowed")]
        public string FirstName { get; set; }
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,20}$",
            ErrorMessage = "Characters are not allowed")]
        public string Title { get; set; }
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,4}$",
            ErrorMessage = "Characters are not allowed")]
        public string TitleOfCourtesy { get; set; }
        [Range(typeof(DateTime), "1/1/1950", "1/1/2001",
        ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime? BirthDate { get; set; }
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,15}$",
            ErrorMessage = "Characters are not allowed")]
        public string City { get; set; }
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,10}$",
            ErrorMessage = "Characters are not allowed")]
        public string Region { get; set; }
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,20}$",
            ErrorMessage = "Characters are not allowed")]
        public string Country { get; set; }
    }
}
