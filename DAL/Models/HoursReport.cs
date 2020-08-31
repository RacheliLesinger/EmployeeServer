using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class HoursReport
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan? TimeStart { get; set; }
        public TimeSpan? TimeEnd { get; set; }
        public int? DayReportType { get; set; }
        public string Comment { get; set; }
        public int EmployeeNumber { get; set; }

        public virtual DayReportType DayReportTypeNavigation { get; set; }
        public virtual Employee EmployeeNumberNavigation { get; set; }
    }
}
