using Microsoft.AspNetCore.Mvc;
using PersonelKayitApi.Model;
using PersonelKayitApi.Services;

namespace PersonelKayitApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeEgitimController : Controller
    {
        public readonly ITestDbApiService _testDbApiService;

        public EmployeeEgitimController(ITestDbApiService testDbApiService)
        {
            _testDbApiService = testDbApiService;
        }

        [HttpGet]
        public async Task<List<EmployeeEgitim>> GetEmployeeEgitimApi(long id)
        {
            var result = _testDbApiService.GetEmployeeEgitimApiService(id);
            return result;
        }
    }
}
