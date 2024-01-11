using System;
using System.Collections.Generic;

namespace PersonelKayitApi.Model
{
    public partial class EmployeeEgitim
    {
        public long Id { get; set; }
        public long EmployeeId { get; set; }
        public long ParamOkullarId { get; set; }
        public short MezuniyetYili { get; set; }
    }
}
