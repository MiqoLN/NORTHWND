using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NORTHWND.Core.Abstractions.Operations;
using NORTHWND.Core.BusinessModels;

namespace NORTHWND.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderDetailsController:ControllerBase
    {
        private readonly IOrderDetailOperations _orderDetailOperations;
        public OrderDetailsController(IOrderDetailOperations orderDetailOperations)
        {
            _orderDetailOperations = orderDetailOperations;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_orderDetailOperations.GetAll());
        }

        [HttpGet("properties")]
        public IActionResult Get([FromQuery] OrderDetailsModel model)
        {
            return Ok(_orderDetailOperations.Get(model));
        }
        [HttpPost]
        public IActionResult Post(OrderDetailRegisterModel model)
        {
            if (ModelState.IsValid)
                _orderDetailOperations.Add(model);
            else
                return BadRequest("Not all parameters have filled");
            return Created("", model);
        }
        [Authorize]
        [HttpDelete]
        public IActionResult Delete([FromBody]OrderDetailDeleteModel model)
        {
            if (ModelState.IsValid)
                _orderDetailOperations.Delete(model);
            else
                return BadRequest();
            return Ok();
        }

    }
}
