using DAL.Models;
using Entity.Models;
using Microsoft.AspNetCore.Mvc;
using SAL.EmployeeService;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employeeResponses = await _employeeService.GetAllEmployeesAsync();
            if (employeeResponses == null || !employeeResponses.Any())
            {
                Console.WriteLine("No employees found.");
                return NoContent();
            }
            return Ok(employeeResponses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var employeeResponse = await _employeeService.GetEmployeeByIdAsync(id);
            return employeeResponse != null ? Ok(employeeResponse) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] EmployeeEntity employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Will return 400 with validation errors
            }
            var output= await _employeeService.AddEmployeeAsync(employee);
            return CreatedAtAction(nameof(Get), new { id = output }, new { id = output });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] EmployeeEntity employee)
        {
            if (id != employee.Id) return BadRequest();
            await _employeeService.UpdateEmployeeAsync(employee);
            return Ok(employee);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound(); // 404 if employee not found
            }

            await _employeeService.DeleteEmployeeAsync(id); // Proceed with deletion
            return NoContent(); // 204 No Content for successful deletion
        }
    }
}
