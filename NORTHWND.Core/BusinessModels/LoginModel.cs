using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NORTHWND.Core.BusinessModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Password is required")]
        //[DataType(DataType.Password)]
        public string Password { get; set; }
    }
}