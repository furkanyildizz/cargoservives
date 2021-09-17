using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerAddress.Business.Abstract;
using CustomerAddress.Business.Dtos.Neighborhood;
using CustomerAddress.Business.Validations.Neighborhood;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAddress.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NeighborhoodController : ControllerBase
    {
        private readonly INeighborhoodService _neighborhoodService;
        public NeighborhoodController(INeighborhoodService neighborhoodService)
        {
            _neighborhoodService = neighborhoodService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<NeighborhoodListDto>>> GetNeighborhood()
        {

            try
            {
                var neighborhoodList = await _neighborhoodService.GetNeighborhoods();
                return Ok(neighborhoodList);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }




        ///////////////////////////
        ///
        [HttpGet("GetNeighborhoodsById/{neighborhoodID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<NeighborhoodListDto>>> GetNeighborhoodsById(int neighborhoodID)
        {

            try
            {
                var neighborhoodObject = await _neighborhoodService.GetNeighborhoodsById(neighborhoodID);
                return Ok(neighborhoodObject);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        ////////////////////////////////
        ///
        [HttpGet("GetNeighborhoodsByDistrictId/{districtID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<NeighborhoodListDto>>> GetNeighborhoodsByDistrictId(int districtID)
        {

            try
            {
                var neighborhoodObject = await _neighborhoodService.GetNeighborhoodsByDistrictId(districtID);
                return Ok(neighborhoodObject);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        //////////
        ///


        /// ///////////////

        [HttpPost("AddNeighboorhood")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> AddNeighborhood(NeighborhoodAddDto neighborhoodAddDto)
        {
            var validator = new NeighborhoodAddValidator(_neighborhoodService);
            var validationResults = validator.Validate(neighborhoodAddDto);
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
                var result = await _neighborhoodService.AddNeighborhood(neighborhoodAddDto);
                if (result == -2)
                {
                    list.Add("Böyle bir mahalle bulunuyor");
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
        [HttpPut("UpdateNeighborhood/{neighborhoodID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> UpdateDistrict(int neighborhoodID, [FromBody] NeighborhoodUpdateDto neighborhoodUpdateDto)
        {
            var list = new List<string>();
            var validator = new NeighborhoodUpdateValidator(_neighborhoodService);
            var validationResults = validator.Validate(neighborhoodUpdateDto);

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
                int result = await _neighborhoodService.UpdateNeighborhood(neighborhoodID, neighborhoodUpdateDto);
                if (result > 0)
                {
                    list.Add("Güncelleme işlemi başarılı.");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else if (result == -1)
                {
                    list.Add($"{neighborhoodID} nolu branş bulunamadı");
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

        [HttpDelete("DeleteNeighborhood/{neighborhoodID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> DeleteNeighborhood(int neighborhoodID)
        {
            var list = new List<string>();
            try
            {
                int result = await _neighborhoodService.DeleteNeighborhood(neighborhoodID);
                if (result > 0)
                {
                    list.Add("Silme işlemi başarılı.");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else if (result == -1)
                {
                    list.Add($"{neighborhoodID} id branş bulunamadı. Silme işlemi başarısız.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });

                }
                else if (result == -2)
                {
                    list.Add("Bu Mahalle bir sokakta yer almaktadır.SİLİNEMEZ");
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
