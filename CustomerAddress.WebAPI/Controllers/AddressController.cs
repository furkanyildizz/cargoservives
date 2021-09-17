using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerAddress.Business.Abstract;
using CustomerAddress.Business.Dtos.Address;
using CustomerAddress.Business.Validations.Address;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAddress.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        private readonly IStreetService _streetService;
        private readonly INeighborhoodService _neighborhoodService;
        private readonly ICityService _cityService;
        private readonly ITownService _townService;
        public AddressController(IAddressService addressService,IStreetService streetService, INeighborhoodService neighborhoodService, ITownService townService , ICityService cityService)
        {
            _addressService = addressService;
            _streetService = streetService;
            _neighborhoodService = neighborhoodService;
            _cityService = cityService;
            _townService = townService;

        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<AddressListDto>>> GetAddress()
        {

            try
            {
                var addressList = await _addressService.GetAddresses();
                return Ok(addressList);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        
        
        ///////////////////////////
        ///
        [HttpGet("GetAddressesById/{addressID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<AddressListDto>>> GetAddressesById(int addressID)
        {

            try
            {
                var addressObject = await _addressService.GetAddressesById(addressID);
                return Ok(addressObject);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        //////////////////////////////////
        /////
        //[HttpGet("GetAddressesByCustomerId/{customerID}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<ActionResult<List<AddressListDto>>> GetAddressesByCustomerId(int customerID)
        //{

        //    try
        //    {
        //        var addressObject = await _addressService.GetAddressesByCustomerId(customerID);
        //        return Ok(addressObject);
        //    }
        //    catch (Exception ex)
        //    {

        //        return BadRequest(ex.Message);
        //    }
        //}
        //////////////////////////////////
        ///
        [HttpGet("GetAddressesByTownId/{townID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<AddressListDto>>> GetAddressesByTownId(int townID)
        {

            try
            {
                var addressObject = await _addressService.GetAddressesByTownId(townID);
                return Ok(addressObject);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        ////////////////////////////////
        ///
        [HttpGet("GetAddressesByCityId/{cityID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<AddressListDto>>> GetAddressesByCityId(int cityID)
        {

            try
            {
                var addressObject = await _addressService.GetAddressesByCityId(cityID);
                return Ok(addressObject);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        ////////////////////////////////
        ///
        [HttpGet("GetAddressesByNeighborhoodId/{neighborhoodID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<AddressListDto>>> GetAddressesByNeighborhoodId(int neighborhoodID)
        {

            try
            {
                var addressObject = await _addressService.GetAddressesByNeighborhoodId(neighborhoodID);
                return Ok(addressObject);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        ////////////////////////////////
        ///
        [HttpGet("GetAddressesByStreetId/{streetID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<AddressListDto>>> GetAddressesByStreetId(int streetID)
        {

            try
            {
                var addressObject = await _addressService.GetAddressesByStreetId(streetID);
                return Ok(addressObject);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        /// ///////////////////////////

        [HttpPost("AddAddress")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> AddAddress(AddressAddDto addressAddDto)
        {
            var validator = new AddressAddValidator(_addressService, _streetService, _neighborhoodService, _cityService, _townService );
            var validationResults = validator.Validate(addressAddDto);
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
                var result = await _addressService.AddAddress(addressAddDto);
                if (result == -2)
                {
                    list.Add("Böyle bir adres bulunmakta");
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
        [HttpPut("UpdateAdress/{addressID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> UpdateAdress(int addressID, [FromBody] AddressUpdateDto addressUpdateDto)
        {
            var list = new List<string>();
            var validator = new AddressUpdateValidator(_addressService, _streetService, _neighborhoodService, _cityService, _townService);
            var validationResults = validator.Validate(addressUpdateDto);

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
                int result = await _addressService.UpdateAddress(addressID, addressUpdateDto);
                if (result > 0)
                {
                    list.Add("Güncelleme işlemi başarılı.");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else if (result == -1)
                {
                    list.Add($"{addressID} nolu branş bulunamadı");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else if (result == -2)
                {
                    list.Add("Böyle bir adres bulunmaktadır");
                    return Ok(new { code = StatusCode(1002), message = list, type = "error" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return BadRequest();
        }

        //////////////////////////////////
        /////

        [HttpDelete("DeleteAdress/{addressID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> DeleteAddress(int addressID)
        {
            var list = new List<string>();
            try
            {
                int result = await _addressService.DeleteAddress(addressID);
                if (result > 0)
                {
                    list.Add("Silme işlemi başarılı.");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else if (result == -1)
                {
                    list.Add($"{addressID} id branş bulunamadı. Silme işlemi başarısız.");
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
