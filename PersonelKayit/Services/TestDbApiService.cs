using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Packaging.Signing;
using PersonelKayitApi.Model;
using System.Linq;

namespace PersonelKayitApi.Services
{
    public interface ITestDbApiService
    {
        public Employee AddEmploye(Employee employee);
        public List<EmployeeViewModel> GetEmployee();
        public Employee UpdateEmploye(Employee employee);
        public List<Adress> GetAdress(int ustid);
        public Task<List<long>> AddPhotoService(List<MedyaKutuphanesi> photos);
        public EmployeeMedyalar AddEmpPhotoApiService(EmployeeMedyalar medya);
        public bool DeletePhotoApiService(long photoIds);
        public List<MedyaKutuphanesi> GetPhotoUrlApiService(long id);
        public bool UpdateDeleteApiService(long id);
        public List<ParamOkullar> GetOkulApiService();
        public bool PostSchoolApiService(List<EmployeeEgitim> schools);
        public List<EmployeeEgitim> GetEmployeeEgitimApiService(long id);
        public bool DeleteSchoolApiService(long employeeId/*, long paramOkullarId*/);
        //public bool UpdateSchoolApiService(List<EmployeeEgitim> schools);
    }

    public class TestDbApiService : ITestDbApiService
    {
        private readonly TestDBContext _dbContext;

        public TestDbApiService(TestDBContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public Employee AddEmploye(Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));

            _dbContext.Employees.Add(employee);
            _dbContext.SaveChanges();

            return employee;
        }

        public EmployeeMedyalar AddEmpPhotoApiService(EmployeeMedyalar medya)
        {
            
            var result =  _dbContext.EmployeeMedyalars.Add(medya);
             _dbContext.SaveChanges();
            return medya;
        }

        public async Task<List<long>> AddPhotoService(List<MedyaKutuphanesi> photos)
        {
            var savedPhotoIds = new List<long>();
            foreach (var photo in photos)
            {
                _dbContext.MedyaKutuphanesis.Add(photo);
                await _dbContext.SaveChangesAsync();

                savedPhotoIds.Add(photo.Id);
            }

            return savedPhotoIds;
        }

        public List<Adress> GetAdress(int UstId)
        {
           
            var result = _dbContext.Adresses.Where(a => a.UstId == UstId).ToList();

            if (result == null)
                throw new Exception("Adress not found for the given ID.");
            return result;
        }

        public List<EmployeeViewModel> GetEmployee()
        {
            if (_dbContext.Employees == null)
                throw new InvalidOperationException();
            var employee = _dbContext.Employees.Where(x=> x.Deleted == "0").ToList();
            var adress = _dbContext.Adresses.ToList();
            var medyaId = _dbContext.EmployeeMedyalars.ToList();
            var medyaName = _dbContext.MedyaKutuphanesis.ToList();
            List<EmployeeViewModel> kisiler = new List<EmployeeViewModel>();

            foreach (var item in employee)
            {
                string CityName = string.Empty;
                string CountryName = string.Empty;
                string a = string.Empty;
                long countryId = 0;
                var cityAdress = adress.FirstOrDefault(x => x.Id == item.City);
                var photoIds = medyaId.LastOrDefault(x=> x.EmployeeId == item.Id);
                if (photoIds != null)
                {
                    var photoName = medyaName.FirstOrDefault(x => x.Id == photoIds.MedyaId);
                    if (photoName != null)
                    {
                        a = photoName.MedyaUrl;

                    }
                }
                if (cityAdress != null)
                {
                    CityName = cityAdress.Tanim;
                    
                    var countryAdress = adress.FirstOrDefault(x => x.Id == cityAdress.UstId);
                    
                    if (countryAdress != null)
                    {
                        CountryName = countryAdress.Tanim;
                        countryId = countryAdress.Id;
                    }
                }

                var kisi = new EmployeeViewModel()
                {
                    Bday = item.Bday,
                    City = CityName,
                    County = CountryName,
                    Explanation = item.Explanation,
                    Gender = item.Gender,
                    Name = item.Name,
                    Photo = a,
                    Surname = item.Surname,
                    Id = item.Id,
                    cityId = item.City,
                    countryId = countryId,
                    Deleted = item.Deleted
                };
                kisiler.Add(kisi);
            }
            return kisiler;
        }

        public List<MedyaKutuphanesi> GetPhotoUrlApiService(long id)
        {
            if (_dbContext.MedyaKutuphanesis == null)
                throw new InvalidOperationException();
            var mediaKutup = _dbContext.MedyaKutuphanesis.ToList();
            var result = _dbContext.EmployeeMedyalars.Where(x=> x.EmployeeId==id).ToList();

            var m = new List<MedyaKutuphanesi>();
            foreach (var item in result)
            {
                string photoUrl = string.Empty;
                string photoName = string.Empty;
                var url = mediaKutup.FirstOrDefault(x=> x.Id==item.MedyaId);
                if (url != null)
                {
                    photoUrl = url.MedyaUrl;
                    photoName = url.MedyaAdi;
                }
                var medya = new MedyaKutuphanesi
                {
                    Id = url.Id,
                    MedyaAdi = photoName,
                    MedyaUrl = photoUrl,
                };
                m.Add(medya);
            }
            return m;
        }

        public Employee UpdateEmploye(Employee employee)
        {
            if (employee == null)
                throw new NotImplementedException();
            _dbContext.Employees.Update(employee);
            _dbContext.SaveChanges();
            return employee;
        }

        public bool DeletePhotoApiService(long photoIds)
        {
            
            //foreach (var photoId in photoIds)
            //{
                var ress = _dbContext.EmployeeMedyalars.FirstOrDefault(x => x.Id == photoIds);
                if (ress != null)
                {
                    var ress1 = _dbContext.MedyaKutuphanesis.FirstOrDefault(x => x.Id == ress.MedyaId);
                    if (ress1 != null)
                    {
                        _dbContext.MedyaKutuphanesis.Remove(ress1);
                        _dbContext.SaveChanges();
                    }
                    _dbContext.EmployeeMedyalars.Remove(ress);
                    _dbContext.SaveChanges();
                }
            //}
                
           
           
            return true;
        }

        public bool UpdateDeleteApiService(long id)
        {
            var employee = _dbContext.Employees.FirstOrDefault(x=> x.Id == id);
            if (employee != null)
            {
                employee.Deleted = "1";
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<ParamOkullar> GetOkulApiService()
        {
            var result = _dbContext.ParamOkullars.ToList();
            return result;
        }

        public bool PostSchoolApiService(List<EmployeeEgitim> schools)
        {
            foreach (var school in schools)
            {
                _dbContext.EmployeeEgitims.Add(school);
                _dbContext.SaveChanges();
            }
            return true;
        }

        public List<EmployeeEgitim> GetEmployeeEgitimApiService(long id)
        {
            var result = _dbContext.EmployeeEgitims.Where(a => a.EmployeeId == id).ToList();

            if (result == null)
                throw new Exception("Adress not found for the given ID.");
            return result;
        }

        public bool DeleteSchoolApiService(long employeeId/*, long paramOkullarId*/)
        {
            var res = _dbContext.EmployeeEgitims.Where(x => x.EmployeeId == employeeId).ToList();

            foreach (var item in res)
            {
                _dbContext.EmployeeEgitims.RemoveRange(item);
                _dbContext.SaveChanges();
            }
            return true;

            //if (res.Any())
            //{
            //    _dbContext.EmployeeEgitims.RemoveRange(res);
            //    _dbContext.SaveChanges();
            //    return true;
            //}
            //return false;
        }

        //public bool UpdateSchoolApiService(List<EmployeeEgitim> schools)
        //{
        //    foreach (var school in schools)
        //    {
        //        _dbContext.EmployeeEgitims.Update(school);
        //        _dbContext.SaveChanges();
        //    }
        //    return true;
        //}
    }
}
