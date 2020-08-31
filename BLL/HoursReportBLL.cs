
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
using DTOMapper;
using DAL.Models;

namespace BLL
{
    public class HoursReportBLL
    {
        public static List<HoursReportDTO> GetHRsForEmployee(int employeeNumber)
        {
            using (EmployeesDBContext db = new EmployeesDBContext())
            {
                List<HoursReport> hrList = db.HoursReport.Where(hr => hr.EmployeeNumber == employeeNumber).ToList();
                List<HoursReportDTO> destination = HoursReportMapper.DALsToDTOs(hrList);
                return destination;
            }
        }

        public static HoursReportDTO AddHR(HoursReportDTO newHR)
        {
            using (EmployeesDBContext db = new EmployeesDBContext())
            {
                HoursReport hr = HoursReportMapper.DTOToDAL(newHR);
                db.HoursReport.Add(hr);
                db.SaveChanges();
                return HoursReportMapper.DALToDTO(hr);
            }
        }

        public static List<DayReportTypeDTO> AddHRsTypes(List<DayReportTypeDTO> newTypes)
        {
            using (EmployeesDBContext db = new EmployeesDBContext())
            {
                newTypes.ForEach(type =>
                {
                    DayReportType dr = DayReportTypeMapper.DTOToDAL(type);
                    db.DayReportType.Add(dr);
                });

                db.SaveChanges();
                return DayReportTypeMapper.DALsToDTOs(db.DayReportType.ToList());

            }
        }

        public static List<DayReportTypeDTO> GetHRsTypes()
        {
            using (EmployeesDBContext db = new EmployeesDBContext())
            {
                return DayReportTypeMapper.DALsToDTOs(db.DayReportType.ToList());
            }
        }

        public static List<HoursReportDTO> UpdateHRsForEmployee(List<HoursReportDTO> updatedHRs, int employeeNumber)
        {
            using (EmployeesDBContext db = new EmployeesDBContext())
            {
                db.HoursReport.RemoveRange(db.HoursReport.Where(hr => hr.EmployeeNumber == employeeNumber));


                updatedHRs.ForEach(hrDTO =>
                {
                    HoursReport hr = HoursReportMapper.DTOToDAL(hrDTO);
                    hr.EmployeeNumber = employeeNumber;
                    db.HoursReport.Add(hr);
                });

                db.SaveChanges();

                return HoursReportMapper.DALsToDTOs(db.HoursReport.Where(hr => hr.EmployeeNumber == employeeNumber).ToList());
            }
        }

        public static bool UpdateHRsForEmployeeFromCamera(string employeeNumber)
        {
            using (EmployeesDBContext db = new EmployeesDBContext())
            {
                var emp = db.Employee.FirstOrDefault(e => e.EmployeeId == employeeNumber);
                if (emp != null)

                {
                    var hrStart = db.HoursReport.FirstOrDefault(hr => hr.EmployeeNumber == emp.EmployeeNumber
                                                        && hr.TimeStart != null && hr.TimeEnd == null);

                    if (hrStart != null) 
                    {
                        hrStart.TimeEnd = DateTime.Now.TimeOfDay;
                        db.HoursReport.Update(hrStart);
                    }
                    else
                    {
                        var drt = db.DayReportType.FirstOrDefault(r => r.Value.Contains("מצלמה"));
                        HoursReport hr = new HoursReport()
                        {
                            Date = DateTime.Now.Date,
                            TimeStart = DateTime.Now.TimeOfDay,
                            DayReportType = drt.Id,
                            EmployeeNumber = emp.EmployeeNumber
                        };
                        db.HoursReport.Add(hr);
                    };

                    db.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        
        public static List<HoursReportDTO> GetMonthlyHrsForEmployee(int id, int month, int year)
        {
            using (EmployeesDBContext db = new EmployeesDBContext())
            {
                List<HoursReport> hrs = db.HoursReport.Where(hr => hr.EmployeeNumber == id && hr.Date.Month == month && hr.Date.Year == year).ToList();
                return HoursReportMapper.DALsToDTOs(hrs);
            }
        }
    }
}