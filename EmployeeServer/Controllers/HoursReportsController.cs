using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using DTO;
using Microsoft.AspNetCore.Mvc;
using DTOMapper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeServer.Controllers
{
   
    [ApiController]
    public class HoursReportsController : ControllerBase
    {

        [HttpGet]
        [Route("api/GetHRsForEmployee/{employeeNumber}")]
        public IActionResult GetHRsForEmployee(int employeeNumber)
        {

            return Ok(HoursReportBLL.GetHRsForEmployee(employeeNumber));
        }


        [HttpPost]
        [Route("api/AddHR")]
        public IActionResult AddHR(HoursReportRequest newHR)
        {
            var hr = HoursReportMapper.HoursReportRequestToDTO(newHR);
            return Ok(HoursReportBLL.AddHR(hr));
        }

        [HttpPost]
        [Route("api/AddHRsTypes")]
        public IActionResult AddHRsTypes(List<DayReportTypeDTO> newTypes)
        {

            return Ok(HoursReportBLL.AddHRsTypes(newTypes));
        }

        [HttpGet]
        [Route("api/GetHRsTypes")]
        public IActionResult GetHRsTypes()
        {
            return Ok(HoursReportBLL.GetHRsTypes());
        }

        [HttpPost]
        [Route("api/UpdateHRsForEmployee/{employeeNumber}")]
        public IActionResult UpdateHRsForEmployee(List<HoursReportRequest> updatedHRs, int employeeNumber)
        {
            List<HoursReportDTO> hrs=new List<HoursReportDTO>();
            foreach(var hr in updatedHRs)
            {
                hrs.Add(HoursReportMapper.HoursReportRequestToDTO(hr));
            }
            return Ok(HoursReportBLL.UpdateHRsForEmployee(hrs, employeeNumber));
        }

        [HttpPost]
        [Route("api/UpdateHRsForEmployeeFromCamera/{employeeNumber}")]
        public IActionResult UpdateHRsForEmployeeFromCamera( string employeeNumber)
        {
            
            return Ok(HoursReportBLL.UpdateHRsForEmployeeFromCamera(
                
                employeeNumber));
        }

        [HttpGet]
        [Route("api/GetMonthlyHrsForEmployee/{id}/year/{year}/month/{month}")]
        public IActionResult GetMonthlyHrsForEmployee(int id, int month, int year)
        {
            return Ok(HoursReportBLL.GetMonthlyHrsForEmployee(id, month, year));
        }
    }
}
