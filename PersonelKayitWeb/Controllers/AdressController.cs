using Microsoft.AspNetCore.Mvc;
using PersonelKayitWeb.Models;
using PersonelKayitWeb.Services;

namespace PersonelKayitWeb.Controllers
{
    public class AdressController : Controller
    {
        public readonly ITestDbService _testDbService;

        public AdressController(ITestDbService testDbService)
        {
            _testDbService = testDbService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAdress(int UstId)
        {

            var result = await _testDbService.GetAdress(UstId);
            return Ok(result);
        }
    }
}
