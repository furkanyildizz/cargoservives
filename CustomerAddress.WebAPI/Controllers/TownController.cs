using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerAddress.Business.Abstract;
using CustomerAddress.Business.Dtos.District;
using CustomerAddress.Business.Validations.District;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAddress.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TownController : ControllerBase
    {
        private readonly ITownService _townService;
        public TownController(ITownService townService)
        {
            _townService = townService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<TownListDto>>> GetTown()
        {

            try
            {
                var townList = await _townService.GetTowns();
                return Ok(townList);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        

        ///////////////////////////
        ///
        [HttpGet("GetTownsById/{townID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<TownListDto>>> GetTownsById(int townID)
        {

            try
            {
                var townObject = await _townService.GetTownsById(townID);
                return Ok(townObject);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        ////////////////////////////////
        ///
        [HttpGet("GetTownsByCityId/{cityID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<TownListDto>>> GetTownsByCityId(int cityID)
        {

            try
            {
                var townObject = await _townService.GetTownsByCityId(cityID);
                return Ok(townObject);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        /// ///////////////

        [HttpPost("AddTown")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> AddTown(TownAddDto townAddDto)
        {
            var validator = new TownAddValidator(_townService);
            var validationResults = validator.Validate(townAddDto);
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
                var result = await _townService.AddTown(townAddDto);
                if (result == -2)
                {
                    list.Add("Böyle bir ilçe bulunuyor");
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
        [HttpPut("UpdateTown/{townID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> UpdateTown(int townID, [FromBody] TownUpdateDto townUpdateDto)
        {
            var list = new List<string>();
            var validator = new TownUpdateValidator(_townService);
            var validationResults = validator.Validate(townUpdateDto);

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
                int result = await _townService.UpdateTown(townID, townUpdateDto);
                if (result > 0)
                {
                    list.Add("Güncelleme işlemi başarılı.");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else if (result == -1)
                {
                    list.Add($"{townID} nolu branş bulunamadı");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else if (result == -2)
                {
                    list.Add("Böyle bir ilçe zaten bulunmakta");
                    return Ok(new { code = StatusCode(1002), message = list, type = "error" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return BadRequest();
        }


        ////////////////////////////////
        ///

        [HttpDelete("DeleteTown/{townID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> DeleteTown(int townID)
        {
            var list = new List<string>();
            try
            {
                int result = await _townService.DeleteTown(townID);
                if (result > 0)
                {
                    list.Add("Silme işlemi başarılı.");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else if (result == -1)
                {
                    list.Add($"{townID} id branş bulunamadı. Silme işlemi başarısız.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });

                }
                else if (result == -2)
                {
                    list.Add("Bu ilçe bir mahallede yer almaktadır.SİLİNEMEZ");
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
