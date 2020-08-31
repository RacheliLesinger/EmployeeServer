using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class HoursReportRequest
    {
        public int Id { get; set; }
        public System.DateTime date { get; set; }
        public string timeStart { get; set; }
        public string timeEnd { get; set; }
        public string dayReportType { get; set; }
        public string comment { get; set; }
        public int employeeNumber { get; set; }
    }
}
