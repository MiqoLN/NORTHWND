using Microsoft.AspNetCore.Mvc;
using NORTHWND.Core.Abstractions.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NORTHWND.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeOperations _employeeOperations;
        public EmployeesController(IEmployeeOperations employeeOperations)
        {
            _employeeOperations = employeeOperations;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_employeeOperations.GetAll());
        }
    }
}
