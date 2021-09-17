using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerAddress.Business.Abstract;
using CustomerAddress.Business.Dtos.CompanyBranch;
using CustomerAddress.Business.Validations.CompanyBranch;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAddress.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyBranchController : ControllerBase
    {
        private readonly ICompanyBranchService _companyBranchService;
        public CompanyBranchController(ICompanyBranchService companyBranchService)
        {
            _companyBranchService = companyBranchService;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<CompanyBranchListDto>>> GetCompanyBranch()
        {

            try
            {
                var companyBranchList = await _companyBranchService.GetCompanyBranches();
                return Ok(companyBranchList);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }




       

        ///////////////////////////
        ///
        [HttpGet("GetCompanyBranchesById/{companyBranchID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<CompanyBranchListDto>>> GetCompanyBranchesById(int companyBranchID)
        {

            try
            {
                var companyBranchObject = await _companyBranchService.GetCompanyBranchesById(companyBranchID);
                return Ok(companyBranchObject);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        ////////////


        /// ///////////////////////////

        [HttpPost("AddCompanyBranch")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> AddCompanyBranch(CompanyBranchAddDto companyBranchAddDto)
        {
            var validator = new CompanyBranchAddValidator(_companyBranchService);
            var validationResults = validator.Validate(companyBranchAddDto);
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
                var result = await _companyBranchService.AddCompanyBranch(companyBranchAddDto);
                if (result == -2)
                {
                    list.Add("Böyle bir Şube zaten mevcut");
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
        [HttpPut("UpdateCompanyBranch/{companyBranchID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> UpdateCompanyBranch(int companyBranchID, [FromBody] CompanyBranchUpdateDto companyBranchUpdateDto)
        {
            var list = new List<string>();
            var validator = new CompanyBranchUpdateValidator(_companyBranchService);
            var validationResults = validator.Validate(companyBranchUpdateDto);

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
                int result = await _companyBranchService.UpdateCompanyBranch(companyBranchID, companyBranchUpdateDto);
                if (result > 0)
                {
                    list.Add("Güncelleme işlemi başarılı.");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else if (result == -1)
                {
                    list.Add($"{companyBranchID} nolu branş bulunamadı");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else if (result == -2)
                {
                    list.Add("Bu isimde Şube mevcut.");
                    return Ok(new { code = StatusCode(1002), message = list, type = "error" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return BadRequest();
        }

        /// <summary>
        /// /////////////////

        /// <returns></returns>
        [HttpDelete("DeleteCompanyBranch/{companyBranchID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> DeleteCompanyBranch(int companyBranchID)
        {
            var list = new List<string>();
            try
            {
                int result = await _companyBranchService.DeleteCompanyBranch(companyBranchID);
                if (result > 0)
                {
                    list.Add("Silme işlemi başarılı.");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else if (result == -1)
                {
                    list.Add($"{companyBranchID} id branş bulunamadı. Silme işlemi başarısız.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });

                }
                else if (result == -2)
                {
                    list.Add(" Bu Şubenin Çalışanı bulunmaktadır.SİLİNEMEZ");
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
