namespace PersonelKayitWeb.Models
{
    public class EmployeeViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public DateTime Bday { get; set; }
        public string Gender { get; set; } = null!;
        public string? County { get; set; }
        public string? City { get; set; }
        public string? Explanation { get; set; }
        public string? Photo { get; set; }
        public long? cityId { get; set; }
        public long? countryId { get; set; }
        public string? Deleted { get; set; }
    }
}
