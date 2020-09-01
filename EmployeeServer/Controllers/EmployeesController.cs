using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BLL;
using DTO;
using System.Net;
using System.Net.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http.Headers;
using DTOMapper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeServer.Controllers
{
    

    [ApiController]
    public class EmployeesController : ControllerBase
    {
 
        private readonly IWebHostEnvironment _appEnvironment;
        public EmployeesController(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }
        [HttpGet]
        [Route("api/GetEmployees")]
        public IActionResult GetEmployees()
        {
            return Ok(EmployeeBLL.GetEmployees());
        }

    

        [HttpPost]
        [Route("api/LoginEmployee")]
        public IActionResult LoginEmployee([FromBody] LoginEmployeeCardentials employeeCardentials)
        {
            EmployeeDTO foundEmp = EmployeeBLL.LoginEmployee(employeeCardentials);
            if (foundEmp == null)
            {
                return NotFound( "עובד לא קיים");
            }
            return Ok(foundEmp);
        }
        [HttpPost]
        [Route("api/UpdateEmployee/{empNumber}")]
        public IActionResult UpdateEmployee([FromBody] EmployeeRequest updatedEmp, int empNumber)
        {
            EmployeeDTO empDTO = EmployeeMapper.EmployeeRequstToDTO(updatedEmp);
            return Ok(EmployeeBLL.UpdateEmployee(empNumber, empDTO));
        }

        [HttpGet]
        [Route("api/GetEmployeeById/{empNumber}")]
        public IActionResult GetEmployeeById(int empNumber)
        {
            return Ok(EmployeeBLL.GetEmployeeById(empNumber));
        }

       
        [Route("api/AddEmployee")]
        public IActionResult AddEmployee([FromBody] EmployeeRequest newEmp)
        {
            EmployeeDTO empDTO = EmployeeMapper.EmployeeRequstToDTO(newEmp);
            var emp = EmployeeBLL.AddEmployee(empDTO);
            var upOk =  UploadedProfileBLL.AddUploadedProfile(emp);
            return Ok(emp);
        }

        [HttpPost]
        [Route("api/RemoveEmployee/{empNumber}")]
        public IActionResult RemoveEmployee(EmployeeDTO updatedEmp, int empNumber)
        {
            return Ok(EmployeeBLL.RemoveEmployee(empNumber, updatedEmp));
        }


        [HttpPost, DisableRequestSizeLimit]
        [Route("api/UploadImage")]
        public IActionResult UploadImage()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok("https://localhost:44370/Images/"+ fileName);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
      
    }
}
