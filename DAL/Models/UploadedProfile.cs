using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class UploadedProfile
    {
        public string UploadedProfileNumber { get; set; }
        public int EmployeeNumber { get; set; }

        public virtual Employee EmployeeNumberNavigation { get; set; }
    }
}
