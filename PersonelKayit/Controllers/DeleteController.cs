using Microsoft.AspNetCore.Mvc;
using PersonelKayitApi.Services;

namespace PersonelKayitApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeleteController : Controller
    {
        public readonly ITestDbApiService _testDbApiService;

        public DeleteController (ITestDbApiService testDbApiService)
        {
            _testDbApiService = testDbApiService;
        }

        [HttpPut]
        public bool UpdatDeleteApi(long id)
        {
            var result = _testDbApiService.UpdateDeleteApiService(id);
            return result;
        }
    }
}
