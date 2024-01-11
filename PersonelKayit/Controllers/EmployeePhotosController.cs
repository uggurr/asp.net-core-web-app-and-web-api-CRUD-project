using Microsoft.AspNetCore.Mvc;
using PersonelKayitApi.Model;
using PersonelKayitApi.Services;

namespace PersonelKayitApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeePhotosController : Controller
    {
        public readonly ITestDbApiService _testDbApiService;

        public EmployeePhotosController(ITestDbApiService testDbApiService)
        {
            _testDbApiService = testDbApiService;
        }

        [HttpPost]
        public IActionResult AddEmpPhoto(List<EmployeeMedyalar> medyalar)
        {
            var e = new EmployeeMedyalar();
            foreach (var medya in medyalar)
            {
                var result = _testDbApiService.AddEmpPhotoApiService(medya);
                e.EmployeeId = result.EmployeeId;
                e.MedyaId = result.MedyaId;
            }
            return Ok(e);
        }

        [HttpDelete]
        public bool DeletePhotoApi([FromQuery]long photoId)
        {
            var result = _testDbApiService.DeletePhotoApiService(photoId);

            
            return result;
        }
    }
}
