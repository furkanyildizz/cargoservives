using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerAddress.Business.Abstract;
using CustomerAddress.Business.Dtos.Status;
using CustomerAddress.Business.Validations.Status;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAddress.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        public IStatusService _statusService;
        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<StatusListDto>>> GetStatuss()
        {

            try
            {
                var statusList = await _statusService.GetStatuses();
                return Ok(statusList);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Add Status")]
        public async Task<ActionResult<string>> AddStatus(StatusAddDto statusAddDto)
        {
            var validator = new StatusAddValidator(_statusService);
            var validationResults = validator.Validate(statusAddDto);
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
                var result = await _statusService.AddStatus(statusAddDto);
                if (result>0)
                {
                    list.Add("İşlem başarılı");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else
                {
                    list.Add("İşlem Başarısız");
                    return Ok(new { code = StatusCode(1001), message = list, type = "" });

                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpPut("UpdateStatus/{statusId}")]
        public async Task<ActionResult<string>> UpdateStatus(StatusUpdateDto statusUpdateDto,int statusId)
        {
            var validator = new StatusUpdateValidator(_statusService);
            var validationResults = validator.Validate(statusUpdateDto);
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
                var result = await _statusService.UpdateStatus(statusId, statusUpdateDto);
                if (result > 0)
                {
                    list.Add("İşlem Başarılı");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else
                {
                    list.Add("İşlem Başarısız");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("DeleteStatus/{statusId}")]
        public async Task<ActionResult<string>> DeleteStatus(int statusId)
        {
           
            var list = new List<string>();
            var result = await  _statusService.DeleteStatus(statusId);
            if (result > 0)
            {
                list.Add("Silme işlemi başarılı");
                return Ok(new { code = StatusCode(1000), message = list, type = "success" });

            }
            else
            {
                list.Add("Silme işlemi başarısız");
                return Ok(new { code = StatusCode(1001), message = list, type = "error" });
            }
           

        }

    }
}
