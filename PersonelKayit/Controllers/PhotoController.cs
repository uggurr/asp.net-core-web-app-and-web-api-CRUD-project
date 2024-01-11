using Microsoft.AspNetCore.Mvc;
using PersonelKayitApi.Model;
using PersonelKayitApi.Services;

namespace PersonelKayitApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : Controller
    {
        public readonly ITestDbApiService _testDbApiService;

        public PhotoController(ITestDbApiService testDbApiService)
        {
            _testDbApiService = testDbApiService;
        }

        [HttpPost]
        public async Task<List<long>> AddPhoto(List<MedyaKutuphanesi> photos)
        {
            var result = await _testDbApiService.AddPhotoService(photos);
            return result;
        }

        [HttpGet]
        public List<MedyaKutuphanesi> GetPhotoUrl(long id)
        {
            var result = _testDbApiService.GetPhotoUrlApiService(id);
            return result;
        }
    }
}
