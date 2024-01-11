using Microsoft.AspNetCore.Mvc;
using PersonelKayitWeb.Services;

namespace PersonelKayitWeb.Controllers
{
    public class DeleteController : Controller
    {
        public readonly ITestDbService _dbService;

        public DeleteController (ITestDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpPut]
        public async Task<bool> UpdateDelete(long id)
        {
            var result = await _dbService.UpdateDelete(id);
            return result;
        }
    }
}
