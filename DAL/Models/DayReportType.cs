using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class DayReportType
    {
        public DayReportType()
        {
            HoursReport = new HashSet<HoursReport>();
        }

        public int Id { get; set; }
        public string Value { get; set; }

        public virtual ICollection<HoursReport> HoursReport { get; set; }
    }
}
