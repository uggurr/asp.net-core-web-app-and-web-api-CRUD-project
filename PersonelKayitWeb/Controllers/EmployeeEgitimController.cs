using Microsoft.AspNetCore.Mvc;
using PersonelKayitWeb.Models;
using PersonelKayitWeb.Services;

namespace PersonelKayitWeb.Controllers
{
    public class EmployeeEgitimController : Controller
    {
        public readonly ITestDbService _dbService;

        public EmployeeEgitimController(ITestDbService dbService)
        {
            _dbService = dbService;
        }
        [HttpGet]
        public async Task<List<EmployeeEgitim>> GetEmployeeEgitim(long id)
        {
            var result = await _dbService.GetEmployeeEgitimService(id);
            return result;
        }
    }
}
