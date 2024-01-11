using Microsoft.AspNetCore.Mvc;
using PersonelKayitWeb.Models;
using PersonelKayitWeb.Services;

namespace PersonelKayitWeb.Controllers
{
    public class ParamOkullarController : Controller
    {
        public readonly ITestDbService _dbService;

        public ParamOkullarController(ITestDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOkul()
        {
            var result = await _dbService.GetOkulService();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostSchool(long employeeId, List<EmployeeEgitim> schools)
        {
            var result = await _dbService.PostSchoolService(employeeId, schools);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSchool(long employeeId/*, List<EmployeeEgitim> schools*/)
        {
            var result = await _dbService.DeleteSchoolService(employeeId/*, schools*/);
            return Ok(result);
        }

        //[HttpPut]
        //public async Task<IActionResult> UpdateSchool(long employeeId, List<EmployeeEgitim> schools)
        //{
        //    var result = await _dbService.UpdateSchoolService(employeeId, schools);
        //    return Ok(result);
        //}
    }
}
