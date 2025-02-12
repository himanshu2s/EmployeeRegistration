using EmployeeRegistration.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeRegistration.Controllers
{
    [ApiController]
    [Route("api/Employees")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAll() => Ok(await _employeeRepository.GetAllAsync());
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> Get(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null) return NotFound();

            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> Create(Employee employee)
        {
            // Can add exception handling and validations as required.
            var newEmployee = await _employeeRepository.AddAsync(employee);

            return Ok(newEmployee);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Employee>> Update(int id, Employee employee)
        {
            // Can add exception handling and validations as required.
            if (id != employee.Id) 
            { 
                return BadRequest();
            }

            await _employeeRepository.UpdateAsync(employee);

            return Ok(employee);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int employeeId)
        {
            // Can add exception handling and validations as required.
            if (employeeId == 0 || !await _employeeRepository.DeleteAsync(employeeId))
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
