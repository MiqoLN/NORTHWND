﻿using Microsoft.AspNetCore.Mvc;
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
    }
}
