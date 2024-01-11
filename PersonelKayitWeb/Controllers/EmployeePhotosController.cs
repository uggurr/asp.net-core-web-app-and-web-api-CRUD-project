using Microsoft.AspNetCore.Mvc;
using PersonelKayitWeb.Models;
using PersonelKayitWeb.Services;

namespace PersonelKayitWeb.Controllers
{
    public class EmployeePhotosController : Controller
    {
        public readonly ITestDbService _testDbService;

        public EmployeePhotosController(ITestDbService testDbService) 
        {
            _testDbService = testDbService;
        }

        [HttpPost]
        public IActionResult AddEmpPhoto(long employeeId, List<long> photoIds)
        {
            var e = new List<EmployeeMedyalar>();
            foreach(long photoId in photoIds)
            {
                var medyalar = new EmployeeMedyalar
                {
                    EmployeeId = employeeId,
                    MedyaId = photoId,
                };
                e.Add(medyalar);
            }

            var result = _testDbService.AddEmpPhoto(e);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<bool> DeletePhoto(List<long> photoIds)
        {
            return await _testDbService.DeletePhotoService(photoIds);
        }
    }
}
