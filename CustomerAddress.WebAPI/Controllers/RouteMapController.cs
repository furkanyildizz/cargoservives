using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerAddress.Business.Abstract;
using CustomerAddress.Business.Dtos.RouteMap;
using CustomerAddress.Business.Validations.RouteMap;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAddress.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteMapController : ControllerBase
    {
        private readonly IRouteMapService _routeMapService;
        public RouteMapController(IRouteMapService routeMapService)
        {
            _routeMapService = routeMapService;
        }

        /// <summary>
        /// ////////////////
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetMap")]
        public async Task<ActionResult<List<RouteMapListDto>>> GetRouteMaps()
        {
            try
            {
                var routeMaps = await _routeMapService.GetRouteMaps();
                return Ok(routeMaps);
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <returns></returns>
        [HttpGet("GetMapName")]
        public async Task<ActionResult<List<RouteMapListDto>>> GetRouteMapsName()
        {
            try
            {
                var routeMaps = await _routeMapService.GetRouteMapsWithName();
                return Ok(routeMaps);
            }
            catch (Exception)
            {

                throw;
            }

        } /// <returns></returns>
        [HttpGet("GetMapNameByPostCode/{postCode}")]
        public async Task<ActionResult<List<RouteMapListDto>>> GetRouteMapsByPostCode(string postCode)
        {
            try
            {
                var routeMaps = await _routeMapService.GetRouteMapsByPostCode(postCode);
                return Ok(routeMaps);
            }
            catch (Exception)
            {

                throw;
            }

        }


        /// <summary>
        /// ///////////////
        /// </summary>

        /// <returns></returns>
        [HttpPost("AddRouteMap")]


        public async Task<ActionResult<string>> AddRouteMap(RouteMapAddDto routeMapAddDto)
        {
            var validator = new RouteMapAddValidator(_routeMapService);
            var validationResults = validator.Validate(routeMapAddDto);
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
                var result = await _routeMapService.AddRouteMap(routeMapAddDto);
                if (result > 0)
                {
                    list.Add("Ekleme işlemi başarılı");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else
                {
                    list.Add("Ekleme işlemi başarısız");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// ///////////////

        /// <returns></returns>

        [HttpPost("ConfirmRouteMap/{PostCode}/{branchId}")]
        public async Task<ActionResult<string>> ConfirmRouteMap(string PostCode, int branchId)
        {
            var list = new List<string>();
            var result = await _routeMapService.ConfirmCargoByPostName(PostCode , branchId);
            if (result > 0)
            {
                list.Add("Onaylama işlemi başarılı");
                return Ok(new { code = StatusCode(1000), message = list, type = "success" });
            }
            else if (result == -1)
            {
                list.Add("Posta kodu bulunamadı");
                return Ok(new { code = StatusCode(1001), message = list, type = "error" });

            }
            else if(result==-5)
            {
                list.Add("Sipariş teslim edilmiş başarısız");
                return Ok(new { code = StatusCode(1001), message = list, type = "error" });
            }
            else
            {
                list.Add("İşlem başarısız");
                return Ok(new { code = StatusCode(1001), message = list, type = "error" });
            }
        }
        /// <summary>
        /// /////
        /// 
        /// </summary>
        
        /// <returns></returns>
        [HttpPut("UpdateRouteMap/{routeMapId}")]

        public async Task<ActionResult<string>> UpdateRouteMap(RouteMapUpdateDto routeMapUpdateDto, int routeMapId)
        {

            var list = new List<string>();
            var validator = new RouteMapUpdateValidator(_routeMapService);
            var validationresult = validator.Validate(routeMapUpdateDto);
            if (!validationresult.IsValid)
            {
                foreach (var error in validationresult.Errors)
                {
                    list.Add(error.ErrorMessage);
                }
                return Ok(new { code = StatusCode(1002), message = list, type = "error" });
            }

            try
            {
                var result = await _routeMapService.UpdateRouteMap(routeMapUpdateDto, routeMapId);
                if (result > 0)
                {
                    list.Add("İşlem Başarılı");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });

                }
                else if (result == -1)
                {
                    list.Add("id bulunamadı");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });

                }
                else
                {
                    list.Add("işlem başarısız");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });

                }
            }
            catch (Exception)
            {

                throw;
            }

        }



        /// <returns></returns>

        [HttpPut("UpdateRouteMapByPostCode/{postCode}")]

        public async Task<ActionResult<string>> UpdateRouteMapByPostCode(RouteMapCodeUpdateDto routeMapCodeUpdateDto,string postCode)
        {

            var list = new List<string>();
            var validator = new RouteMapByPostUpdateValidator(_routeMapService);
            var validationresult = validator.Validate(routeMapCodeUpdateDto);
            if (!validationresult.IsValid)
            {
                foreach (var error in validationresult.Errors)
                {
                    list.Add(error.ErrorMessage);
                }
                return Ok(new { code = StatusCode(1002), message = list, type = "error" });
            }

            try
            {
                var result =await _routeMapService.UpdateRouteMapByPostCode(routeMapCodeUpdateDto, postCode);
                if (result>0)
                {
                    list.Add("İşlem Başarılı");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });

                }
                else if (result == -1){
                    list.Add("id bulunamadı");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });

                }
                else
                {
                    list.Add("işlem başarısız");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });

                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// ///////////////////////////
        /// </summary>

        /// <returns></returns>

        [HttpDelete("DeleteRouteMap/{RouteMapId}")]
        public async Task<ActionResult<string>> DeleteRouteMap(int RouteMapId)
        {
            var list = new List<string>();
            var result = await _routeMapService.DeleteRouteMap(RouteMapId);
            if (result > 0)
            {
                list.Add("Silme işlemi başarılı");
                return Ok(new { code = StatusCode(1000), message = list, type = "success" });
            }
            else if (result == -1){
                list.Add("id bulunamadı");
                return Ok(new { code = StatusCode(1001), message = list, type = "error" });

            }
            else
            {
                list.Add("işlem başarısız");
                return Ok(new { code = StatusCode(1001), message = list, type = "error" });
            }
        }

        

    }
}
