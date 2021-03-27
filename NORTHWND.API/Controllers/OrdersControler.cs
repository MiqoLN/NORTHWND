using Microsoft.AspNetCore.Mvc;
using NORTHWND.Core.Abstractions.Operations;
using NORTHWND.Core.BusinessModels;
using NORTHWND.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        [HttpGet]
        public IActionResult GetOrders()
        {
            return Ok(_orderOperations.GetOrders());
        }

    }
}
