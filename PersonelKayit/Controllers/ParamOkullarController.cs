using Microsoft.AspNetCore.Mvc;
using PersonelKayitApi.Model;
using PersonelKayitApi.Services;

namespace PersonelKayitApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParamOkullarController : Controller
    {
        private readonly ITestDbApiService _testDbApiService;

        public ParamOkullarController(ITestDbApiService testDbApiService)
        {
            _testDbApiService = testDbApiService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOkulApi()
        {
            var result = _testDbApiService.GetOkulApiService();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostSchoolApi(List<EmployeeEgitim> schools)
        {
            var result = _testDbApiService.PostSchoolApiService(schools);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<bool> DeleteSchool(long employeeId/*, long paramOkullarId*/)
        {
            var result = _testDbApiService.DeleteSchoolApiService(employeeId/*, paramOkullarId*/);
            return result;
        }

        //[HttpPut]
        //public async Task<bool> UpdateSchoolApi(List<EmployeeEgitim> schools)
        //{
        //    var result = _testDbApiService.UpdateSchoolApiService(schools);
        //    return result;
        //}
    }
}
