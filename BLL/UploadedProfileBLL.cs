
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
using DTOMapper;
using System.Dynamic;
using DAL.Models;
using Services;

namespace BLL
{
    public class UploadedProfileBLL
    {

        public async static Task<bool> AddUploadedProfile(EmployeeDTO newEmp)
        {
            var personID = await AzureFaceService.AddPersonAndFaceToPersonGroup(newEmp.employeeId, newEmp.imageUrl, newEmp.employeeNumber);
            using (EmployeesDBContext db = new EmployeesDBContext())
            {
                UploadedProfile up = new UploadedProfile();
                up.EmployeeNumber = newEmp.employeeNumber;
                up.UploadedProfileNumber = personID.ToString();

                db.UploadedProfile.Add(up);
                db.SaveChanges();
                return true;
            }
            return true;
        }
    }
}