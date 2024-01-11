using PersonelKayitWeb.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

namespace PersonelKayitWeb.Services
{
    public interface ITestDbService
    {
        public Task<Employee> AddEmployee(Employee employee);
        public Task<List<EmployeeViewModel>> GetEmployeeService();
        public Task<Employee> UpdateEmployee(Employee employee);
        public Task<List<Adress>> GetAdress(int UstId);
        public Task<List<long>> AddPhotoService(List<MedyaKutuphanesi> photo);
        public Task<IActionResult> AddEmpPhoto(List<EmployeeMedyalar> medyalar);
        public Task<bool> DeletePhotoService(List<long> photoId);
        public Task<List<MedyaKutuphanesi>> GetPhotoUrlService(long id);
        public Task<bool> UpdateDelete(long id);
        public Task<List<ParamOkullar>> GetOkulService();
        public Task<IActionResult> PostSchoolService(long employeeId, List<EmployeeEgitim> shools);
        public Task<List<EmployeeEgitim>> GetEmployeeEgitimService(long id);
        public Task<bool> DeleteSchoolService(long employeeId/*, List<EmployeeEgitim> shools*/);
        //public Task<bool> UpdateSchoolService(long employeeId, List<EmployeeEgitim> schools);
    }

    public class TestDbService : ITestDbService
    {
        private readonly HttpClient _httpClient1;
        private readonly HttpClient _httpClient2;
        private readonly HttpClient _httpClient3;
        private readonly HttpClient _httpClient4;
        private readonly HttpClient _httpClient5;
        private readonly HttpClient _httpClient6;
        private readonly HttpClient _httpClient7;
        public TestDbService(HttpClient httpClient1, HttpClient httpClient2, HttpClient httpClient3, HttpClient httpClient4, HttpClient httpClient5, HttpClient httpClient6, HttpClient httpClient7)
        {
            _httpClient1 = httpClient1;
            _httpClient2 = httpClient2;
            _httpClient3 = httpClient3;
            _httpClient4 = httpClient4;
            _httpClient5 = httpClient5;
            _httpClient6 = httpClient6;
            _httpClient7 = httpClient7;
            _httpClient1.BaseAddress = new Uri("https://localhost:44357/api/Employee");
            _httpClient2.BaseAddress = new Uri("https://localhost:44357/api/Adress");
            _httpClient3.BaseAddress = new Uri("https://localhost:44357/api/Photo");
            _httpClient4.BaseAddress = new Uri("https://localhost:44357/api/EmployeePhotos");
            _httpClient5.BaseAddress = new Uri("https://localhost:44357/api/Delete");
            _httpClient6.BaseAddress = new Uri("https://localhost:44357/api/ParamOkullar");
            _httpClient7.BaseAddress = new Uri("https://localhost:44357/api/EmployeeEgitim");
        }
        public async Task<Employee> AddEmployee(Employee employee)
        {
            var jsonData = JsonConvert.SerializeObject(employee);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await _httpClient1.PostAsync("Employee", content);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Employee>(responseData);
                return result;
            }
            else
            {
                throw new Exception("Employee gönderme işlemi başarısız oldu. HTTP status code: " + response.StatusCode);
            }
        }

        public async Task<IActionResult> AddEmpPhoto(List<EmployeeMedyalar> medyalar)
        {
            var jsonData = JsonConvert.SerializeObject(medyalar);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await _httpClient4.PostAsync("EmployeePhotos", content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<IActionResult>();
                return result;
            }
            else
            {
                throw new Exception("Photo update failed. HTTP Status code: " + response.StatusCode);
            }
        }

        public async Task<List<long>> AddPhotoService(List<MedyaKutuphanesi> photos)
        {
            var jsonData = JsonConvert.SerializeObject(photos);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await _httpClient3.PostAsync("Photo", content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<long>>();
                return result;
            }
            else
            {
                throw new Exception("Photo update failed. HTTP Status code: " + response.StatusCode);
            }
        }

        public async Task<List<Adress>> GetAdress(int UstId)
        {
            var response = await _httpClient2.GetAsync($"Adress?ustid={UstId}");

            if (response.IsSuccessStatusCode)
            {
                var responsAdressData = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Adress>>(responsAdressData);
                return result;
            }
            else
            {
                throw new Exception("Veriler alınamadı. HTTP Status code: " + response.StatusCode);
            }
        }

        public async Task<List<EmployeeViewModel>> GetEmployeeService()
        {
            var response = await _httpClient1.GetAsync("Employee");

            if (response.IsSuccessStatusCode)
            {
                var responsData = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<EmployeeViewModel>>(responsData);
                return result;
            }
            else
            {
                throw new Exception("Veriler alınamadı. HTTP Status code: " + response.StatusCode);
            }
        }

        public async Task<List<MedyaKutuphanesi>> GetPhotoUrlService(long id)
        {
            var response = await _httpClient3.GetAsync($"Photo?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var responsAdressData = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<MedyaKutuphanesi>>(responsAdressData);
                return result;
            }
            else
            {
                throw new Exception("Veriler alınamadı. HTTP Status code: " + response.StatusCode);
            }
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            var jsonData = JsonConvert.SerializeObject(employee);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await _httpClient1.PutAsync("Employee", content);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var updatedEmployee = JsonConvert.DeserializeObject<Employee>(responseData);
                return updatedEmployee;
            }
            else
            {
                throw new Exception("Employee update failed. HTTP Status code: " + response.StatusCode);
            }
        }

        public async Task<bool> DeletePhotoService(List<long> photoIds)
        {
            foreach (var photoId in photoIds)
            {
                var result = await _httpClient4.DeleteAsync($"EmployeePhotos?photoId={photoId}");

                if (!result.IsSuccessStatusCode)
                {
                    throw new Exception("Veriler alınamadı. HTTP Status code: " + result.StatusCode);
                }
            }

            return true;
        }

        public async Task<bool> UpdateDelete(long id)
        {
            var jsonData = JsonConvert.SerializeObject(id);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await _httpClient5.PutAsync("Delete?id=" + id, content);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var updatedEmployee = JsonConvert.DeserializeObject<bool>(responseData);
                return updatedEmployee;
            }
            else
            {
                throw new Exception("Employee update failed. HTTP Status code: " + response.StatusCode);
            }
        }

        public async Task<List<ParamOkullar>> GetOkulService()
        {
            var response = await _httpClient6.GetAsync("ParamOkullar");

            if (response.IsSuccessStatusCode)
            {
                var responsData = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<ParamOkullar>>(responsData);
                return result;
            }
            else
            {
                throw new Exception("Veriler alınamadı. HTTP Status code: " + response.StatusCode);
            }
        }

        public async Task<IActionResult> PostSchoolService(long employeeId, List<EmployeeEgitim> schools)
        {
            var e = new List<EmployeeEgitim>();
            foreach (var school in schools)
            {
                var medya = new EmployeeEgitim
                {
                    EmployeeId = employeeId,
                    ParamOkullarId = school.ParamOkullarId,
                    MezuniyetYili = school.MezuniyetYili,
                };
                e.Add(medya);
            }
            var jsonData = JsonConvert.SerializeObject(e);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await _httpClient6.PostAsync("ParamOkullar", content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<IActionResult>();
                return result;
            }
            else
            {
                throw new Exception("Photo update failed. HTTP Status code: " + response.StatusCode);
            }
        }

        public async Task<List<EmployeeEgitim>> GetEmployeeEgitimService(long id)
        {
            var response = await _httpClient7.GetAsync($"EmployeeEgitim?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var responsAdressData = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<EmployeeEgitim>>(responsAdressData);
                return result;
            }
            else
            {
                throw new Exception("Veriler alınamadı. HTTP Status code: " + response.StatusCode);
            }
        }

        public async Task<bool> DeleteSchoolService(long employeeId/*, List<EmployeeEgitim> schools*/)
        {
            
                var result = await _httpClient6.DeleteAsync($"ParamOkullar?employeeId={employeeId}");

                if (!result.IsSuccessStatusCode)
                {
                    throw new Exception("Veriler alınamadı. HTTP Status code: " + result.StatusCode);
                }
            

            return true;

            //foreach (var school in schools)
            //{
            //    var url = $"ParamOkullar?employeeId={employeeId}&paramOkullarId={school.ParamOkullarId}";
            //    var response = await _httpClient6.DeleteAsync(url);

            //    if (response.IsSuccessStatusCode)
            //    {
            //        var result = await response.Content.ReadFromJsonAsync<bool>();
            //        return result;
            //    }
            //    else
            //    {
            //        throw new Exception("Photo update failed. HTTP Status code: " + response.StatusCode);
            //    }
            //}

            //return false;
        }

    //    public async Task<bool> UpdateSchoolService(long employeeId, List<EmployeeEgitim> schools)
    //    {
    //        var e = new List<EmployeeEgitim>();
    //        foreach (var school in schools)
    //        {
    //            var medya = new EmployeeEgitim
    //            {
    //                EmployeeId = employeeId,
    //                ParamOkullarId = school.ParamOkullarId,
    //                MezuniyetYili = school.MezuniyetYili,
    //            };
    //            e.Add(medya);
    //        }
    //        var jsonData = JsonConvert.SerializeObject(e);
    //        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
    //        var response = await _httpClient6.PutAsync("ParamOkullar", content);

    //        if (response.IsSuccessStatusCode)
    //        {
    //            var result = await response.Content.ReadFromJsonAsync<bool>();
    //            return result;
    //        }
    //        else
    //        {
    //            throw new Exception("Photo update failed. HTTP Status code: " + response.StatusCode);
    //        }
    //    }
    }
}
