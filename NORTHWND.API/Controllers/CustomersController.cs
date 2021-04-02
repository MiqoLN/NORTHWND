using Microsoft.AspNetCore.Mvc;
using NORTHWND.Core.Abstractions.Operations;
using NORTHWND.Core.BusinessModels;

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
            return Ok(_customerOperations.Get());
        }
        [HttpGet("properties")]
        public IActionResult Get([FromQuery] CustomerViewModel model)
        {
            return Ok(_customerOperations.Get(model));
        }
        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] string id)
        {
            return Ok(_customerOperations.Get(id));
        }
        [HttpPost]
        public IActionResult Post([FromBody] CustomerRegisterModel model)
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
        public IActionResult GetCustomersWithoutOrder([FromRoute] int id)
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
