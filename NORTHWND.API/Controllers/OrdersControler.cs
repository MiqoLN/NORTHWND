using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NORTHWND.Core.Abstractions.Operations;
using NORTHWND.Core.BusinessModels;

namespace NORTHWND.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderOperations _orderOperations;
        public OrdersController(IOrderOperations orderOperations)
        {
            _orderOperations = orderOperations;
        }
        [HttpGet("properties")]
        public IActionResult Get([FromQuery] OrderViewModel model)
        {
            return Ok(_orderOperations.Get(model));
        }
        [Authorize(Roles = "User")]
        [HttpGet]
        public IActionResult GetOrders()
        {
            return Ok(_orderOperations.GetOrders());
        }
        [HttpGet("{id}")]
        public IActionResult GetOrder([FromRoute] int id)
        {
            return Ok(_orderOperations.GetOrder(id));
        }

        [HttpGet("inventory")]
        public IActionResult GetInventoryList()
        {
            var res = _orderOperations.GetInventoryList();
            return Ok(res);
        }
        [HttpGet("month/ending")]
        public IActionResult GetMonthEndOrders()
        {
            var res = _orderOperations.GetMonthEndOrders();
            return Ok(res);
        }
        [HttpGet("random")]
        public IActionResult GetRandomOrders()
        {
            var res = _orderOperations.GetRandomOrders();
            return Ok(res);
        }
        [HttpGet("doubled")]
        public IActionResult GetDoubledOrders()
        {
            var res = _orderOperations.GetDoubledOrders();
            return Ok(res);

        }
        [HttpGet("total")]
        public IActionResult GetTotalOrders()
        {
            var res = _orderOperations.GetTotalOrders();
            return Ok(res);
        }
        [HttpGet("late")]
        public IActionResult GetLateOrders()
        {
            var res = _orderOperations.GetLateOrders();
            return Ok(res);
        }
        [HttpGet("country/{country}")]
        public IActionResult GetOrdersByCountry([FromRoute] string country)
        {
            var res = _orderOperations.GetOrdersByCountry(country);
            return Ok(res);
        }
        [HttpPut]
        public IActionResult Edit([FromQuery] OrderChangeModel model)
        {
            _orderOperations.EditOrder(model);
            return Ok();
        }
        [HttpPost]
        public IActionResult Add([FromBody] OrderRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                _orderOperations.AddOrder(model);
            }
            else
            {
                return BadRequest();
            }
            return Ok();
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _orderOperations.Delete(id);
            return Ok();
        }

    }
}
