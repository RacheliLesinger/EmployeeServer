
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

namespace BLL
{
    public class EmployeeBLL
    {

        public static List<EmployeeDTO> GetEmployees()
        {
            using (EmployeesDBContext db = new EmployeesDBContext())
            {
                List<Employee> empList = db.Employee.ToList();
                List<EmployeeDTO> destination = EmployeeMapper.DALsToDTOs(empList);
                return destination;
            }
        }

        public static EmployeeDTO AddEmployee(EmployeeDTO newEmp)
        {
            using (EmployeesDBContext db = new EmployeesDBContext())
            {
                Employee emp = EmployeeMapper.DTOToDAL(newEmp);
                emp.DateAdded = DateTime.Now;
                db.Employee.Add(emp);
                db.SaveChanges();
                return EmployeeMapper.DALToDTO(emp);
            }
        }

        public static EmployeeDTO LoginEmployee(LoginEmployeeCardentials employeeCardentials)
        {
            using (EmployeesDBContext db = new EmployeesDBContext())
            {
                Employee foundEmp = db.Employee.First(emp => emp.Email == employeeCardentials.email && emp.EmployeeId == employeeCardentials.employeeId);
                if (foundEmp != null) { return EmployeeMapper.DALToDTO(foundEmp); };
                return null;

            }
        }

        public static async Task<object> UpdateEmployee(int empNumber, EmployeeDTO updatedEmp)
        {
            try
            {

                using (EmployeesDBContext db = new EmployeesDBContext())
                {
                    Employee oldEmp = db.Employee.FirstOrDefault(e => e.EmployeeNumber == empNumber);

                    Employee emp = EmployeeMapper.DTOToDAL(updatedEmp);
                    if (oldEmp.ImageUrl != emp.ImageUrl)
                        await UploadedProfileBLL.AddUploadedProfile(updatedEmp);
                    emp.DateAdded = oldEmp.DateAdded;
                    db.Employee.Update(emp);
                    db.SaveChanges();
                    return EmployeeMapper.DALToDTO(emp);
                }

            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static EmployeeDTO GetEmployeeById(int empNumber)
        {
            using (EmployeesDBContext db = new EmployeesDBContext())
            {
                Employee emp = db.Employee.FirstOrDefault(empl => empl.EmployeeNumber == empNumber);
                return EmployeeMapper.DALToDTO(emp);
            }
        }
    }
}