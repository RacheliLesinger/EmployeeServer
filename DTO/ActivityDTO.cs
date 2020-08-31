using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ActivityDTO
    {
        public int activityId { get; set; }
        public int employeeNumber { get; set; }
        public System.DateTime activityDate { get; set; }
        public bool activityStatus { get; set; }
    }
}
