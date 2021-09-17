using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerAddress.Business.Dtos.RouteMap;
using CustomerAddress.DAL.Context;

namespace CustomerAddress.Business.Abstract
{
    public interface IRouteMapService
    {


        public Task<List<RouteMapNameListDto>> GetRouteMapsWithName();
        public Task<List<RouteMapNameListDto>> GetRouteMapsByPostCode(string postCode);
        public Task<int> ConfirmCargoByPostName(string PostCode, int BranchId);
        public Task<List<RouteMapListDto>> GetRouteMaps();
        public Task<int> AddRouteMap(RouteMapAddDto routeMapAddDto);
        public Task<int> UpdateRouteMap(RouteMapUpdateDto routeMapUpdateDto,int routeMapId);
        public Task<int> DeleteRouteMap(int routeMapId);
        public Task<int> UpdateRouteMapByPostCode(RouteMapCodeUpdateDto routeMapCodeUpdateDto, string postCode);
        public bool  AnyPost(int postId);


    }
}
