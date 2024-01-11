namespace PersonelKayitWeb.Models
{
    public class Employee
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public DateTime Bday { get; set; }
        public string Gender { get; set; } = null!;
        public long? Country { get; set; }
        public long? City { get; set; }
        public string? Explanation { get; set; }
        public long? Photo { get; set; }
        public string? Deleted { get; set; }
    }
}
