using Microsoft.AspNetCore.Http;

namespace PersonelKayitWeb.Models
{
    public class MedyaKutuphanesi
    {
        public long Id { get; set; }
        public string MedyaAdi { get; set; } = null!;
        public string MedyaUrl { get; set; } = null!;
    }
}
