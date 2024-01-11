using System;
using System.Collections.Generic;

namespace PersonelKayitApi.Model
{
    public partial class EmployeeMedyalar
    {
        public long Id { get; set; }
        public long EmployeeId { get; set; }
        public long MedyaId { get; set; }
    }
}
