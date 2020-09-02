using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Models;
using DTO;

namespace DTOMapper
{
    public class HoursReportMapper
    {
        public static HoursReportDTO DALToDTO(HoursReport hr)
        {
            return new HoursReportDTO
            {
                comment = hr.Comment,
                date = hr.Date,
                dayReportType = (int)hr.DayReportType,
                employeeNumber = hr.EmployeeNumber,
                Id = hr.Id,
                timeEnd = hr.TimeEnd.ToString(),
                timeStart =hr.TimeStart.ToString()
            };
        }
        public static List<HoursReportDTO> DALsToDTOs(List<HoursReport> hrs)
        {

            List<HoursReportDTO> DTOList = new List<HoursReportDTO>();
            hrs.ForEach(emp => DTOList.Add(DALToDTO(emp)));
            return DTOList;
        }

        public static HoursReport DTOToDAL(HoursReportDTO hr)
        {
            return new HoursReport
            {
                Comment = hr.comment,
                Date = hr.date,
                DayReportType = hr.dayReportType,
                EmployeeNumber = hr.employeeNumber,
                Id = hr.Id,
                TimeEnd = TimeSpan.Parse(hr.timeEnd),
                TimeStart = TimeSpan.Parse(hr.timeStart)
            };
        }

        public static HoursReportDTO HoursReportRequestToDTO(HoursReportRequest hr)
        {
            return new HoursReportDTO
            {
                comment = hr.comment,
                date = DateTime.ParseExact(hr.date, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                dayReportType = int.Parse(hr.dayReportType),
                employeeNumber = hr.employeeNumber,
                Id = hr.Id,
                timeEnd = hr.timeEnd,
                timeStart = hr.timeStart
            };
        }
    }
}
