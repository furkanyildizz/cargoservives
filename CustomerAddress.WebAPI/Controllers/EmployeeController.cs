using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerAddress.Business.Abstract;
using CustomerAddress.Business.Dtos.Employee;
using CustomerAddress.Business.Validations.Employee;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAddress.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<EmployeeListDto>>> GetEmployee()
        {

            try
            {
                var employeeList = await _employeeService.GetEmployees();
                return Ok(employeeList);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

       
        ///////////////////////////
        ///
        [HttpGet("GetEmployessById/{employeeID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<EmployeeListDto>>> GetEmployessById(int employeeID)
        {

            try
            {
                var employeeObject = await _employeeService.GetEmployeesById(employeeID);
                return Ok(employeeObject);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        ////////////////////////
        ///
        /// ///////////////////////////

        [HttpPost("AddEmployee")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> AddEmployee(EmployeeAddDto employeeAddDto)
        {
            var validator = new EmployeeAddValidator(_employeeService);
            var validationResults = validator.Validate(employeeAddDto);
            var list = new List<string>();

            if (!validationResults.IsValid)
            {
                foreach (var error in validationResults.Errors)
                {
                    list.Add(error.ErrorMessage);
                }
                return Ok(new { code = StatusCode(1002), message = list, type = "error" });
            }
            try
            {
                var result = await _employeeService.AddEmployee(employeeAddDto);
                if (result == -2)
                {
                    list.Add("Böyle bir çalışan zaten mevcut");
                    return Ok(new { code = StatusCode(1002), message = list, type = "error" });
                }
                else
                {
                    list.Add("Ekleme işlemi başarılı.");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }



        //////////////////////////////////
        ///
        [HttpPut("UpdateEmployee/{employeeID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> UpdateEmployee(int employeeID, [FromBody] EmployeeUpdateDto employeeUpdateDto)
        {
            var list = new List<string>();
            var validator = new EmployeeUpdateValidator(_employeeService);
            var validationResults = validator.Validate(employeeUpdateDto);

            if (!validationResults.IsValid)
            {
                foreach (var error in validationResults.Errors)
                {
                    list.Add(error.ErrorMessage);
                }
                return Ok(new { code = StatusCode(1002), message = list, type = "error" });
            }
            try
            {
                int result = await _employeeService.UpdateEmployee(employeeID, employeeUpdateDto);
                if (result > 0)
                {
                    list.Add("Güncelleme işlemi başarılı.");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else if (result == -1)
                {
                    list.Add($"{employeeID} nolu branş bulunamadı");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else if (result == -2)
                {
                    list.Add("Bu isimde çalışan mevcut.");
                    return Ok(new { code = StatusCode(1002), message = list, type = "error" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return BadRequest();
        }



        /// //////////////

        [HttpDelete("DeleteEmployee/{employeeID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> DeleteEmployee(int employeeID)
        {
            var list = new List<string>();
            try
            {
                int result = await _employeeService.DeleteEmployee(employeeID);
                if (result > 0)
                {
                    list.Add("Silme işlemi başarılı.");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else if (result == -1)
                {
                    list.Add($"{employeeID} id branş bulunamadı. Silme işlemi başarısız.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });

                }
                else if (result == -2)
                {
                    list.Add(" Bu çalışanın postu bulunmaktadır.SİLİNEMEZ");
                    return Ok(new { code = StatusCode(1002), message = list, type = "error" });
                }
                else
                {
                    return BadRequest();
                }



            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
