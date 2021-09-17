using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerAddress.Business.Abstract;
using CustomerAddress.Business.Dtos.Street;
using CustomerAddress.Business.Validations.Street;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAddress.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StreetController : ControllerBase
    {
        private readonly IStreetService _streetService;
        public StreetController(IStreetService streetService)
        {
            _streetService = streetService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<StreetListDto>>> GetStreet()
        {

            try
            {
                var streetList = await _streetService.GetStreets();
                return Ok(streetList);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        ///////////////////////////
        ///
        [HttpGet("GetStreetsById/{streetID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<StreetListDto>>> GetStreetsById(int streetID)
        {

            try
            {
                var streetObject = await _streetService.GetStreetsById(streetID);
                return Ok(streetObject);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        ////////////////////////////////
        ///
        [HttpGet("GetStreetsByNeighborhoodId/{neighborhoodID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<StreetListDto>>> GetStreetsByNeighborhoodId(int neighborhoodID)
        {

            try
            {
                var streetObject = await _streetService.GetStreetsByNeighborhoodId(neighborhoodID);
                return Ok(streetObject);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        ////////////////
        ///

        [HttpPost("AddStreet")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> AddStreet(StreetAddDto streetAddDto)
        {
            var validator = new StreetAddValidator(_streetService);
            var validationResults = validator.Validate(streetAddDto);
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
                var result = await _streetService.AddStreet(streetAddDto);
                if (result == -2)
                {
                    list.Add("Böyle bir sokak bulunuyor");
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
        [HttpPut("UpdateStreet/{streetID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> UpdateDistrict(int streetID, [FromBody] StreetUpdateDto streetUpdateDto)
        {
            var list = new List<string>();
            var validator = new StreetUpdateValidator(_streetService);
            var validationResults = validator.Validate(streetUpdateDto);

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
                int result = await _streetService.UpdateStreet(streetID, streetUpdateDto);
                if (result > 0)
                {
                    list.Add("Güncelleme işlemi başarılı.");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else if (result == -1)
                {
                    list.Add($"{streetID} nolu branş bulunamadı");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else if (result == -2)
                {
                    list.Add("Böyle bir sokak zaten bulunmakta");
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

        [HttpDelete("DeleteStreet/{streetID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> DeleteStreet(int streetID)
        {
            var list = new List<string>();
            try
            {
                int result = await _streetService.DeleteStreet(streetID);
                if (result > 0)
                {
                    list.Add("Silme işlemi başarılı.");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else if (result == -1)
                {
                    list.Add($"{streetID} id branş bulunamadı. Silme işlemi başarısız.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });

                }
                else if (result == -2)
                {
                    list.Add("Bu sokak bir adresde yer almaktadır.SİLİNEMEZ");
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
