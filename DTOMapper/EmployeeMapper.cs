using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Models;
using DTO;

namespace DTOMapper
{
    public class EmployeeMapper
    {

        public static EmployeeDTO DALToDTO(Employee emp)
        {
            return new EmployeeDTO
            {
                firstName = emp.FirstName,
                lastName = emp.LastName,
                dateAdded = emp.DateAdded,
                employeeId = emp.EmployeeId,
                employeeNumber = emp.EmployeeNumber,
                imageUrl = emp.ImageUrl,
                numUploadedProfiles = emp.NumUploadedProfiles,
                email = emp.Email,
                phone = emp.Phone,
                hoursPerDay =(TimeSpan)emp.HoursPerDay ,
                maximumExtraHours = (TimeSpan)emp.MaximumExtraHours
            };
        }
        public static List<EmployeeDTO> DALsToDTOs(List<Employee> emps)
        {

            List<EmployeeDTO> DTOList = new List<EmployeeDTO>();
            emps.ForEach(emp => DTOList.Add(DALToDTO(emp)));
            return DTOList;
        }

        public static Employee DTOToDAL(EmployeeDTO emp)
        {
            return new Employee
            {
                FirstName = emp.firstName,
                LastName = emp.lastName,
                DateAdded = emp.dateAdded,
                EmployeeId = emp.employeeId,
                EmployeeNumber = emp.employeeNumber,
                ImageUrl = emp.imageUrl,
                NumUploadedProfiles = emp.numUploadedProfiles,
                Email = emp.email,
                Phone = emp.phone,
                HoursPerDay =  emp.hoursPerDay,
                MaximumExtraHours = emp.maximumExtraHours
            };
        }

        public static EmployeeDTO EmployeeRequstToDTO(EmployeeRequest emp)
        {
            return new EmployeeDTO
            {
                firstName = emp.firstName,
                lastName = emp.lastName,
                dateAdded = emp.dateAdded,
                employeeId = emp.employeeId,
                employeeNumber = emp.employeeNumber,
                imageUrl = emp.imageUrl,
                numUploadedProfiles = emp.numUploadedProfiles,
                email = emp.email,
                phone = emp.phone,
                hoursPerDay = TimeSpan.Parse(emp.hoursPerDay),
                maximumExtraHours = TimeSpan.Parse(emp.maximumExtraHours)
            };
        }

    }
}
