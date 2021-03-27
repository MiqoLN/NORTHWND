using Microsoft.AspNetCore.Mvc;
using NORTHWND.Core.Abstractions.Operations;
using NORTHWND.Core.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NORTHWND.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerOperations _customerOperations;
        public CustomersController(ICustomerOperations customerOperations)
        {
            _customerOperations = customerOperations;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_customerOperations.GetAll());
        }
        [HttpPost]
        public IActionResult Post([FromBody] CustomerRegistrationModel model)
        {
            if (ModelState.IsValid)
                _customerOperations.Add(model);
            else
                return BadRequest();
            return Ok();
        }
    }
}
