using Microsoft.AspNetCore.Mvc;
using NORTHWND.Core.Abstractions.Operations;
using NORTHWND.Core.BusinessModels;

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
        [HttpGet("categories")]
        public IActionResult GetCategories()
        {
            var res = _productOperations.GetCategories();
            return Ok(res);
        }
        [HttpGet("toreorder")]
        public IActionResult GetReorderingProducts()
        {
            var res = _productOperations.GetReorderingProducts();
            return Ok(res);
        }
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
