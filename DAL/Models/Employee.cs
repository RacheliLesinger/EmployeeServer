using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Activity = new HashSet<Activity>();
            HoursReport = new HashSet<HoursReport>();
            UploadedProfile = new HashSet<UploadedProfile>();
        }

        public int EmployeeNumber { get; set; }
        public string EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DateAdded { get; set; }
        public int NumUploadedProfiles { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public TimeSpan? HoursPerDay { get; set; }
        public TimeSpan? MaximumExtraHours { get; set; }

        public virtual ICollection<Activity> Activity { get; set; }
        public virtual ICollection<HoursReport> HoursReport { get; set; }
        public virtual ICollection<UploadedProfile> UploadedProfile { get; set; }
    }
}
