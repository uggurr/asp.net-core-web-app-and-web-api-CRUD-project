using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using PersonelKayitApi.Model;
using PersonelKayitApi.Services;

namespace PersonelKayitApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdressController : ControllerBase
    {
        public readonly ITestDbApiService _testDbApiService;

        public AdressController (ITestDbApiService testDbApiService)
        {
            _testDbApiService = testDbApiService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAdress(int UstId)
        {
          
            var result = _testDbApiService.GetAdress(UstId);
            return Ok(result);
        }


    }
}
