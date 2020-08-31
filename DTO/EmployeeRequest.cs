using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class EmployeeRequest
    {
        public int employeeNumber { get; set; }
        public string employeeId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public System.DateTime dateAdded { get; set; }
        public int numUploadedProfiles { get; set; }
        public string imageUrl { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string hoursPerDay { get; set; }
        public string maximumExtraHours { get; set; }
    }
}
