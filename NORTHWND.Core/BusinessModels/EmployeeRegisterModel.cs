using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NORTHWND.Core.BusinessModels
{
    public class EmployeeRegisterModel
    {
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string TitleOfCourtesy { get; set; }
        [Required]
        public DateTime? BirthDate { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Region { get; set; }
        [Required]
        public string Country { get; set; }

    }
}
