using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerAddress.Business.Abstract;
using CustomerAddress.Business.Dtos.City;
using CustomerAddress.Business.Validations.City;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAddress.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;
        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<CityListDto>>> GetCity()
        {

            try
            {
                var cityList = await _cityService.GetCities();
                return Ok(cityList);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        

        ///////////////////////////
        ///
        [HttpGet("GetCitiesById/{cityID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<CityListDto>>> GetCitiesById(int cityID)
        {

            try
            {
                var cityObject = await _cityService.GetCitiesById(cityID);
                return Ok(cityObject);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        //////////////////
        ///


        /// ///////////////////////////

        [HttpPost("AddCity")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> AddCity(CityAddDto cityAddDto)
        {
            var validator = new CityAddValidator(_cityService);
            var validationResults = validator.Validate(cityAddDto);
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
                var result = await _cityService.AddCity(cityAddDto);
                if (result == -2)
                {
                    list.Add("Böyle bir şehir zaten mevcut");
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
        [HttpPut("UpdateCity/{cityID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> UpdateCity(int cityID, [FromBody] CityUpdateDto cityUpdateDto)
        {
            var list = new List<string>();
            var validator = new CityUpdateValidator(_cityService);
            var validationResults = validator.Validate(cityUpdateDto);

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
                int result = await _cityService.UpdateCity(cityID, cityUpdateDto);
                if (result > 0)
                {
                    list.Add("Güncelleme işlemi başarılı.");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else if (result == -1)
                {
                    list.Add($"{cityID} nolu branş bulunamadı");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else if (result == -2)
                {
                    list.Add("Bu isimde şehir mevcut.");
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

        [HttpDelete("DeleteCity/{cityID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> DeleteCity(int cityID)
        {
            var list = new List<string>();
            try
            {
                int result = await _cityService.DeleteCity(cityID);
                if (result > 0)
                {
                    list.Add("Silme işlemi başarılı.");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else if (result == -1)
                {
                    list.Add($"{cityID} id branş bulunamadı. Silme işlemi başarısız.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });

                }
                else if (result == -2)
                {
                    list.Add(" Bu ilin ilçesi bulunmaktadır.SİLİNEMEZ");
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
