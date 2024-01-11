using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonelKayitApi.Model;
using PersonelKayitApi.Services;

namespace PersonelKayitApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ITestDbApiService _testDbApiService;

        public EmployeeController(ITestDbApiService testDbApiService)
        {
            _testDbApiService = testDbApiService;
        }

        [HttpPost]
        public async Task<ActionResult> PostEmployee(Employee employee)
        {
            var result = _testDbApiService.AddEmploye(employee);
            return Ok(employee);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployee()
        {
            var result = _testDbApiService.GetEmployee();
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(Employee employee)
        {
            var result = _testDbApiService.UpdateEmploye(employee);
            return Ok(result);
        }
    }
}
