using System;
using System.Collections.Generic;

namespace PersonelKayitApi.Model
{
    public partial class MedyaKutuphanesi
    {
        public long Id { get; set; }
        public string MedyaAdi { get; set; } = null!;
        public string MedyaUrl { get; set; } = null!;
    }
}
