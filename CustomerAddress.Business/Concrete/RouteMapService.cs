using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerAddress.Business.Abstract;
using CustomerAddress.Business.Dtos.RouteMap;
using CustomerAddress.DAL.Context;
using CustomerAddress.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerAddress.Business.Concrete
{
    public class RouteMapService : IRouteMapService
    {
        private readonly CargoDbContext _cargoDbContext;
        public RouteMapService(CargoDbContext cargoDbContext)
        {
            _cargoDbContext = cargoDbContext;
        }
        public async Task<int> AddRouteMap(RouteMapAddDto routeMapAddDto)
        {
            var routeMap = new RouteMap
            {
                CompanyBranchId = routeMapAddDto.CompanyBranchId,
                PostId = routeMapAddDto.PostId,
                StatusId = routeMapAddDto.StatusId,
                CreatedDate = DateTime.Now.ToLocalTime()
            };
            _cargoDbContext.RouteMaps.Add(routeMap);
            return await _cargoDbContext.SaveChangesAsync();


        }

        public  bool AnyPost(int postId)
        {
            return  _cargoDbContext.Posts.Where(p => !p.IsDeleted).Any(p => p.Id == postId);
        }

        public async Task<int> UpdateRouteMapByPostCode(RouteMapCodeUpdateDto routeMapCodeUpdateDto, string postCode)
        {
            var postId = _cargoDbContext.Posts.Where(p => !p.IsDeleted && p.PostCode == postCode).Select(p => p.Id).FirstOrDefault();
            var result = await _cargoDbContext.RouteMaps.Where(p => !p.IsDeleted && p.Id == postId).FirstOrDefaultAsync();
            if (result != null)
            {
                result.CompanyBranchId = routeMapCodeUpdateDto.CompanyBranchId;
                result.PostId = await _cargoDbContext.Posts.Where(p => !p.IsDeleted && p.PostCode == postCode).Select(p => p.Id).FirstOrDefaultAsync();
                result.StatusId = routeMapCodeUpdateDto.StatusId;
                _cargoDbContext.RouteMaps.Update(result);
                return await _cargoDbContext.SaveChangesAsync();
            }
            else
            {
                return await Task.FromResult(-1);
            }

        }

        
        public async Task<int> ConfirmCargoByPostName(string PostCode,int BranchId)
        {
            var post = await _cargoDbContext.Posts.Where(p => !p.IsDeleted && p.PostCode == PostCode).FirstOrDefaultAsync();
            if (post.Id > 0)
            {
                var routeMap = await _cargoDbContext.RouteMaps.Where(p => !p.IsDeleted && p.PostId == post.Id).OrderBy(p => p.CreatedDate).LastOrDefaultAsync();
                var routeAdd = new RouteMap();
                if (routeMap != null)
                {

                    routeAdd.CompanyBranchId = BranchId;
                    routeAdd.StatusId = routeMap.StatusId + 1;
                    routeAdd.PostId = routeMap.PostId;
                    var statusIdList = _cargoDbContext.Statuses.Where(p => !p.IsDeleted).Select(p => p.Id).ToList();
                    if (statusIdList.Contains(routeAdd.StatusId))
                    {
                        if (routeAdd.StatusId == 3)
                        {
                            post.PostStatus = 2;
                            post.ShippingFinishDate = DateTime.Now.ToLocalTime();
                            _cargoDbContext.Posts.Update(post);
                        }
                        routeAdd.CreatedDate = DateTime.Now.ToLocalTime();
                        _cargoDbContext.RouteMaps.Add(routeAdd);
                        return await _cargoDbContext.SaveChangesAsync();
                    }
                    else
                    {
                        return await Task.FromResult(-5);
                    }

                }
                else
                {
                    return await Task.FromResult(-1);
                }

            }
            else
            {
                return await Task.FromResult(-6);
            }
            
          
          
        }
        
        //public async Task<int> UpdateRouteMapByPostCode(RouteMapCodeUpdateDto routeMapCodeUpdateDto, string postCode)
        //{
        //    var postId = _cargoDbContext.Posts.Where(p => !p.IsDeleted && p.PostCode == postCode).Select(p => p.Id).FirstOrDefault();
        //    var result = await _cargoDbContext.RouteMaps.Where(p => !p.IsDeleted && p.Id == postId).FirstOrDefaultAsync();
        //    if (result != null)
        //    {
        //        result.CompanyBranchId = routeMapCodeUpdateDto.CompanyBranchId;
        //        result.PostId = await _cargoDbContext.Posts.Where(p => !p.IsDeleted && p.PostCode == routeMapCodeUpdateDto.PostCode).Select(p => p.Id).FirstOrDefaultAsync();
        //        result.StatusId = routeMapCodeUpdateDto.StatusId;
        //        _cargoDbContext.RouteMaps.Update(result);
        //        return await _cargoDbContext.SaveChangesAsync();
        //    }
        //    else
        //    {
        //        return await Task.FromResult(-1);
        //    }

        //}

        public async Task<int> DeleteRouteMap(int routeMapId)
        {
            var routeMapObject = await _cargoDbContext.RouteMaps.Where(p => !p.IsDeleted && p.Id == routeMapId).SingleOrDefaultAsync();
            if (routeMapObject != null)
            {
                routeMapObject.IsDeleted = true;
                _cargoDbContext.RouteMaps.Update(routeMapObject);
                return await _cargoDbContext.SaveChangesAsync();
            }
            else
            {
                return await Task.FromResult(-1);
            }
        }

        public async Task<List<RouteMapListDto>> GetRouteMaps()
        {
            return await _cargoDbContext.RouteMaps.Where(p => !p.IsDeleted).Select(p => new RouteMapListDto
            {
                Id = p.Id,
                CompanyBranchId = p.CompanyBranchId,
                PostId = p.PostId,
                StatusId = p.StatusId,
                CreatedDate = p.CreatedDate
            }).ToListAsync();
        }
        public async Task<List<RouteMapNameListDto>> GetRouteMapsWithName()
        {
            
            return await _cargoDbContext.RouteMaps.Where(p => !p.IsDeleted).Include(p=>p.CompanyBranchFK).Include(p=>p.StatusFK).Include(p=>p.PostFK).Select(p=> new RouteMapNameListDto { 
                Id=p.Id,
                CompanyBranchName=p.CompanyBranchFK.BranchName,
                PostCode=p.PostFK.PostCode,
                CreatedDate=p.CreatedDate,
                StatusDescription=p.StatusFK.Description            
            }).ToListAsync();
            

        }
        
        public async Task<List<RouteMapNameListDto>> GetRouteMapsByPostCode(string postCode)
        {
            var postId = await _cargoDbContext.Posts.Where(p => !p.IsDeleted && p.PostCode==postCode).Select(p => p.Id).FirstOrDefaultAsync();
            
            return await _cargoDbContext.RouteMaps.Where(p => !p.IsDeleted&& p.PostId==postId).Include(p=>p.CompanyBranchFK).Include(p=>p.StatusFK).Include(p=>p.PostFK).Select(p=> new RouteMapNameListDto { 
                Id=p.Id,
                CompanyBranchName=p.CompanyBranchFK.BranchName,
                PostCode=p.PostFK.PostCode,
                CreatedDate=p.CreatedDate,
                StatusDescription=p.StatusFK.Description            
            }).ToListAsync();
            

        }


        public async Task<int> UpdateRouteMap(RouteMapUpdateDto routeMapUpdateDto, int routeMapId)
        {
            var result = await _cargoDbContext.RouteMaps.Where(p => !p.IsDeleted && p.Id == routeMapId).FirstOrDefaultAsync();
            if (result != null)
            {
                result.CompanyBranchId = routeMapUpdateDto.CompanyBranchId;
                result.PostId = routeMapUpdateDto.PostId;
                result.StatusId = routeMapUpdateDto.StatusId;
                _cargoDbContext.RouteMaps.Update(result);
                return await _cargoDbContext.SaveChangesAsync();
            }
            else
            {
                return await Task.FromResult(-1);
            }
        }

        //public async Task<int> ConfirmRouteMapByPostCode(int companyBranchId, string PostCode)
        //{
        //    var result =await _cargoDbContext.Posts.Where(p => !p.IsDeleted).AnyAsync(p => p.PostCode == PostCode);
        //    if (result)
        //    {
        //        var RouteMap = new RouteMapAddDto
        //        {
        //            CompanyBranchId = companyBranchId,
        //            Post
        //        }
        //    }
        //}
    }
}
