using Microsoft.AspNetCore.Mvc;
using NORTHWND.Core.Abstractions;
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
    public class OrderDetailsController:ControllerBase
    {
        private readonly IOrderDetailOperations _orderDetailOperations;
        public OrderDetailsController(IOrderDetailOperations orderDetailOperations)
        {
            _orderDetailOperations = orderDetailOperations;
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
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_orderDetailOperations.GetAll());
        }
    }
}
