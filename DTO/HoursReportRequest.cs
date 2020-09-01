using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class HoursReportRequest
    {
        public int Id { get; set; }
        public string date { get; set; }
        public string timeStart { get; set; }
        public string timeEnd { get; set; }
        public string dayReportType { get; set; }
        public string comment { get; set; }
        public int employeeNumber { get; set; }
        public string totalHours { get; set; }

        public string usualHours { get; set; }
        public string extraHours { get; set; }



    }
}
