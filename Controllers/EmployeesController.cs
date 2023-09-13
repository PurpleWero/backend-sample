using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuProyecto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly FirebaseClient _firebaseClient;

        public EmployeesController(FirebaseClient firebaseClient)
        {
            _firebaseClient = firebaseClient;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> Get()
        {
            var employees = await _firebaseClient.Child("employees").OnceAsync<Employee>();
            return employees.Select(x => x.Object).ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetById(string id)
        {
            var employee = await _firebaseClient.Child("employees").Child(id).OnceSingleAsync<Employee>();

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> Create(Employee employee)
        {
            var post = await _firebaseClient.Child("employees").PostAsync(employee);
            return CreatedAtAction(nameof(GetById), new { id = post.Key }, employee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Employee updatedEmployee)
        {
            var employee = await _firebaseClient.Child("employees").Child(id).OnceSingleAsync<Employee>();

            if (employee == null)
            {
                return NotFound();
            }

            // Actualiza los datos del empleado

            await _firebaseClient.Child("employees").Child(id).PutAsync(updatedEmployee);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var employee = await _firebaseClient.Child("employees").Child(id).OnceSingleAsync<Employee>();

            if (employee == null)
            {
                return NotFound();
            }

            await _firebaseClient.Child("employees").Child(id).DeleteAsync();

            return NoContent();
        }
    }
}
