using Microsoft.AspNetCore.Mvc;
using NORTHWND.Core.Abstractions.Operations;
using NORTHWND.Core.BusinessModels;

namespace NORTHWND.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeOperations _employeeOperations;
        public EmployeesController(IEmployeeOperations employeeOperations)
        {
            _employeeOperations = employeeOperations;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_employeeOperations.Get());
        }
        [HttpGet("properties")]
        public IActionResult Get([FromQuery] EmployeeViewModel model)
        {
            return Ok(_employeeOperations.Get(model));
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            return Ok(_employeeOperations.Get(id));
        }
        [HttpPost]
        public IActionResult Post([FromBody]EmployeeRegisterModel model)
        {
            if (ModelState.IsValid)
                _employeeOperations.AddEmployee(model);
            else
                return BadRequest("Not all parameters have filled");
            
            return Created("",model);
        }
        [HttpPut]
        public IActionResult Edit([FromQuery]EmployeeChangeModel model)
        {
            _employeeOperations.EditEmployee(model);
            return Ok();
        }
        [HttpGet("late")]
        public IActionResult GetLateEmployees()
        {
            var res = _employeeOperations.GetLateEmployees();
            return Ok(res);
        }
    }
}
