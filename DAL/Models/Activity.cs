using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Activity
    {
        public int ActivityId { get; set; }
        public int EmployeeNumber { get; set; }
        public DateTime ActivityDate { get; set; }
        public bool ActivityStatus { get; set; }

        public virtual Employee EmployeeNumberNavigation { get; set; }
    }
}
