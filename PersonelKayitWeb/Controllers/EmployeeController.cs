using Microsoft.AspNetCore.Mvc;
using PersonelKayitWeb.Models;
using Newtonsoft.Json;
using PersonelKayitWeb.Services;
using NuGet.Protocol;

namespace PersonelKayitWeb.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ITestDbService _dbService;

        public EmployeeController(ITestDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public IActionResult Employee()
        {
            return View();
        }
        public IActionResult EmployeeList()
        {
            return View();
        }

        public IActionResult EditPage()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<long>> AddEmployee(Employee employee)
        {
            
            var result = await _dbService.AddEmployee(employee);
            //var jsonEmployee = JsonConvert.SerializeObject(employee);
            return result.Id;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployee()
        {
            var result = await _dbService.GetEmployeeService();
            return Ok(result);
        }
   

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(Employee employee)
        {
            var result = await _dbService.UpdateEmployee(employee);
            return Ok(employee);
        }
    }
}
