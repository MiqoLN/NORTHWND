using NORTHWND.Core.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace NORTHWND.Core.Entities
{
    public class User
    {

        public int Id { get; set; }
        public Role Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
