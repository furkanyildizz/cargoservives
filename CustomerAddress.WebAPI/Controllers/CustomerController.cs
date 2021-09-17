using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerAddress.Business.Abstract;
using CustomerAddress.Business.Dtos.Customer;
using CustomerAddress.Business.Validations.Customer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAddress.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
            public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<CustomerListDto>>> GetCustomer()
        {

            try
            {
                var customerList = await _customerService.GetCustomers();
                return Ok(customerList);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }



       
        ///////////////////////////
        ///
        [HttpGet("GetCustomersById/{customerID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<CustomerListDto>>> GetCustomersById(int customerID)
        {

            try
            {
                var customerObject = await _customerService.GetCustomersById(customerID);
                return Ok(customerObject);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        ///////////////////////////
        ///
        [HttpGet("GetCustomersByBranchId/{branchID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<CustomerListDto>>> GetCustomersByBranchId(int branchID)
        {

            try
            {
                var customerObject = await _customerService.GetCustomersByCompanyBranchId(branchID);
                return Ok(customerObject);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }




        ///////////////////////////
        ///
        [HttpGet("GetCustomersByAddressId/{addressID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<CustomerListDto>>> GetCustomersByStreetId(int addressID)
        {
    

            try
            {
                var customerList = await _customerService.GetCustomersByAddressId(addressID);
                return Ok(customerList);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        ///////////////////////
        ///

        /// ///////////////////////////

        [HttpPost("AddCustomer")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> AddCustomer(CustomerAddDto customerAddDto)
        {
            var validator = new CustomerAddValidator(_customerService);
            var validationResults = validator.Validate(customerAddDto);
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
                await _customerService.AddCustomer(customerAddDto);
                list.Add("Ekleme işlemi başarılı.");
                return Ok(new { code = StatusCode(1000), message = list, type = "success" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        //////////////////////////////////
        ///
        [HttpPut("UpdateCustomer/{customerID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> UpdateCustomer(int customerID, [FromBody] CustomerUpdateDto customerUpdateDto)
        {
            var list = new List<string>();
            var validator = new CustomerUpdateValidator(_customerService);
            var validationResults = validator.Validate(customerUpdateDto);

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
                int result = await _customerService.UpdateCustomer(customerID, customerUpdateDto);
                if (result > 0)
                {
                    list.Add("Güncelleme işlemi başarılı.");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else if (result == -1)
                {
                    list.Add($"{customerID} nolu branş bulunamadı");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return BadRequest();
        }


        /// //////////////

        [HttpDelete("DeleteCustomer/{customerID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> DeleteCustomer(int customerID)
        {
            var list = new List<string>();
            try
            {
                int result = await _customerService.DeleteCustomer(customerID);
                if (result > 0)
                {
                    list.Add("Silme işlemi başarılı.");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else if (result == -1)
                {
                    list.Add($"{customerID} id branş bulunamadı. Silme işlemi başarısız.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });

                }
                else if (result == -2)
                {
                    list.Add("Bu Müşterinin Adresi bulunmaktadır");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
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
