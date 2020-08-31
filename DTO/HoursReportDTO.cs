using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class HoursReportDTO
    {
        public int Id { get; set; }
        public System.DateTime date { get; set; }
        public Nullable<System.TimeSpan> timeStart { get; set; }
        public Nullable<System.TimeSpan> timeEnd { get; set; }
        public Nullable<int> dayReportType { get; set; }
        public string comment { get; set; }
        public int employeeNumber { get; set; }
    }
}
