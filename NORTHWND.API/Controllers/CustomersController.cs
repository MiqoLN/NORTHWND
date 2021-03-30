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
        [HttpPut]
        public IActionResult Edit([FromQuery] CustomerChangeModel model)
        {
            if (ModelState.IsValid)
                _customerOperations.EditCustomer(model);
            else return BadRequest();
            return Ok();
        }
        [HttpGet("without-order/{id}")]
        public IActionResult GetCustomersWithoutOrder([FromRoute]int id)
        {
            return Ok(_customerOperations.GetCustomersWithNoOrders(id));
        }
        [HttpGet("vip")]
        public IActionResult GetVipCustomers()
        {
            var res = _customerOperations.GetVipCustomers();
            return Ok(res);
        }
        [HttpGet("groups/count")]
        public IActionResult GetCustomerGroup()
        {
            var res = _customerOperations.GetCustomersGroup();
            return Ok(res);
        }
        [HttpGet("no-order")]
        public IActionResult GetCustomersWithNoOrders()
        {
            var res = _customerOperations.GetCustomersWithNoOrders();
            return Ok(res);
        }
    }
}
