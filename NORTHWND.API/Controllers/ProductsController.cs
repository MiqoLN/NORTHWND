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
    public class ProductsController : ControllerBase
    {
        private readonly IProductOperations _productOperations;
        public ProductsController(IProductOperations productOperations)
        {
            _productOperations = productOperations;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_productOperations.Get());
        }
        [HttpGet("properties")]
        public IActionResult Get([FromQuery] ProductViewModel model)
        {
            return Ok(_productOperations.Get(model));
        }
        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            return Ok(_productOperations.Get(id));
        }
        /*
        [HttpGet("categories")]
        public IActionResult GetCategories()
        {
            var products = _dbContext.Products.AsQueryable();
            var categories = _dbContext.Categories.AsQueryable();
            var res = from p in products
                      group p by p.CategoryId into pp
                      orderby pp.Count() descending
                      select new
                      {
                          Id = pp.Key,
                          Count = pp.Count()
                      };
            var final = (from r in res
                         join c in categories
                         on r.Id equals c.CategoryId
                         select new
                         {
                             Name = c.CategoryName,
                             Count = r.Count
                         }).ToList();
            return Ok(final);
        }
        [HttpGet("toreorder")]
        public IActionResult GetReorderingProducts()
        {
            var products = _dbContext.Products.AsQueryable();
            var res = (from p in products
                       where (p.UnitsInStock + p.UnitsOnOrder < p.ReorderLevel) && p.Discontinued == false
                       orderby p.ProductId
                       select p).ToList();
            return Ok(res);
        }*/
        [HttpPost]
        public IActionResult Post([FromBody] ProductRegisterModel model)
        {
            if (ModelState.IsValid)
                _productOperations.Add(model);
            else
                return BadRequest("Not all properties have filled");
            return Created("", model);
        }
        [HttpPut]
        public IActionResult Edit([FromQuery] ProductChangeModel model)
        {
            if (ModelState.IsValid)
                _productOperations.Edit(model);
            else return BadRequest();
            return Ok();
        }
    }
}
