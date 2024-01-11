using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PersonelKayitWeb.Models;
using PersonelKayitWeb.Services;

namespace PersonelKayitWeb.Controllers
{
    public class PhotoController : Controller
    {
        public readonly ITestDbService _testDbService;
        

        public PhotoController(ITestDbService testDbService)
        {
            _testDbService = testDbService;
        }

        [HttpPost]
        public async Task<List<long>> AddPhoto(IFormCollection files)
        {
            List<string> fileNames = new List<string>();
            var sonuc = new List<long>();
            //List<long> savedPhotoIds = new List<long>();
            var m = new List<MedyaKutuphanesi>();
            foreach (var file in files.Files)
            {
                if (file.Length > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var newImageName = Guid.NewGuid() + fileName;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/MedyaKutuphanesi/", newImageName);


                    var stream = new FileStream(filePath, FileMode.Create);
                    file.CopyTo(stream);
                    

                    fileNames.Add(newImageName);
                    var medya = new MedyaKutuphanesi
                    {
                        
                        MedyaUrl = newImageName,
                        MedyaAdi = fileName
                    };

                    m.Add(medya);
                }
                
            }
            var result = _testDbService.AddPhotoService(m);
            return result.Result;
        }

        [HttpGet]
        public async Task<IActionResult> GetPhotoUrl(long id)
        {
            var result = await _testDbService.GetPhotoUrlService(id);
            return Ok(result);
        }
    }
}
