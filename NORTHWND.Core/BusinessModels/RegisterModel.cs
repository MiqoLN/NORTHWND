using System.ComponentModel.DataAnnotations;

namespace NORTHWND.Core.BusinessModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        //[DataType(DataType.Password)]
        public string Password { get; set; }

        //[DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = " Password is incorrect")]
        public string ConfirmPassword { get; set; }
    }
}
