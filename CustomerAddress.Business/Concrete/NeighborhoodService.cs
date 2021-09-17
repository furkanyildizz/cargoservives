using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerAddress.Business.Abstract;
using CustomerAddress.Business.Dtos.Neighborhood;
using CustomerAddress.DAL.Context;
using CustomerAddress.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerAddress.Business.Concrete
{
    public class NeighborhoodService : INeighborhoodService
    {
        private readonly CargoDbContext _customerAddressContext;
        public NeighborhoodService(CargoDbContext customerAddressContext)
        {
            _customerAddressContext = customerAddressContext;
        }
        public async Task<int> AddNeighborhood(NeighborhoodAddDto neighborhoodAddDto)
        {
            var result = await AnyNeighborhood(neighborhoodAddDto);
            if (result)
            {
                return await Task.FromResult(-2);
            }
            else
            {
                var newNeighborhood = new Neighborhood
                {
                    NeighborhoodName = neighborhoodAddDto.NeighborhoodName,
                    TownId = neighborhoodAddDto.TownId

                };
                _customerAddressContext.Neighborhoods.Add(newNeighborhood);
                await _customerAddressContext.SaveChangesAsync();
                return await Task.FromResult(1);

            }
        }

        public bool AnyTownId(int townId)
        {
            return _customerAddressContext.Towns.Where(p => !p.IsDeleted).Any(p => p.Id == townId);
        }

        public async Task<bool> AnyNeighborhood(NeighborhoodAddDto neighborhoodAddDto)
        {
            return await _customerAddressContext.Neighborhoods.Where(p => !p.IsDeleted).AnyAsync(p => p.NeighborhoodName == neighborhoodAddDto.NeighborhoodName&&p.TownId==neighborhoodAddDto.TownId);
        }
        public async Task<int> DeleteNeighborhood(int id)
        {
            var result = await _customerAddressContext.Streets.Where(p => !p.IsDeleted).AnyAsync(p => p.NeighborhoodId == id);
            var NeighborhoodObject = _customerAddressContext.Neighborhoods.SingleOrDefault(p => !p.IsDeleted && p.Id == id);
            if (result)
            {
                return await Task.FromResult(-2);
            }
            else
            {
                if (NeighborhoodObject != null)
                {
                    NeighborhoodObject.IsDeleted = true;
                    _customerAddressContext.Neighborhoods.Update(NeighborhoodObject);
                    return await _customerAddressContext.SaveChangesAsync();
                }
                else
                {
                    return await Task.FromResult(-1);
                }
            }
        }

        public Task<List<NeighborhoodListDto>> GetNeighborhoods()
        {

            return _customerAddressContext.Neighborhoods.Include(p => p.TownFK).Where(p => !p.IsDeleted).Select(p => new NeighborhoodListDto
            {
                Id = p.Id,
                TownName = p.TownFK.TownName,
                NeighborhoodName = p.NeighborhoodName


            }).ToListAsync();
        }

        public bool AnyNeighborhoodId(int id)
        {

            return _customerAddressContext.Neighborhoods.Where(p => !p.IsDeleted).Any(p => p.Id == id);
        }

        public async Task<List<NeighborhoodListDto>> GetNeighborhoodsByDistrictId(int id)
        {
            return await _customerAddressContext.Neighborhoods.Where(p => !p.IsDeleted && p.TownId == id).Include(p => p.TownFK)
                .Select(p => new NeighborhoodListDto
                {
                    Id = p.Id,
                    TownName = p.TownFK.TownName,
                    NeighborhoodName = p.NeighborhoodName

                }).ToListAsync();
        }

        public Task<NeighborhoodListDto> GetNeighborhoodsById(int id)
        {
            return _customerAddressContext.Neighborhoods.Where(p => !p.IsDeleted).Include(p => p.TownFK)
          .Select(p => new NeighborhoodListDto
          {
              Id = p.Id,
              NeighborhoodName = p.NeighborhoodName,
              TownName = p.TownFK.TownName

          }).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> AnyNeighborhoodUpdate(NeighborhoodUpdateDto neighborhoodUpdateDto, int id)
        {
            string townName = _customerAddressContext.Towns.Where(p => !p.IsDeleted && p.Id == neighborhoodUpdateDto.TownId).Select(p => p.TownName).FirstOrDefault();
            var result = await _customerAddressContext.Neighborhoods.Where(p => !p.IsDeleted && p.Id != id).Include(p => p.TownFK).AnyAsync(p => p.NeighborhoodName == neighborhoodUpdateDto.NeighborhoodName && p.TownFK.TownName == townName);
            return result;
        }


        public async Task<int> UpdateNeighborhood(int id, NeighborhoodUpdateDto neighborhoodUpdateDto)
        {
            var NeighborhoodObject = await _customerAddressContext.Neighborhoods.SingleOrDefaultAsync(p => !p.IsDeleted && p.Id == id);
            var result = await AnyNeighborhoodUpdate(neighborhoodUpdateDto, id);



            if (NeighborhoodObject == null)
            {
                return await Task.FromResult(-1);
            }

            NeighborhoodObject.TownId = neighborhoodUpdateDto.TownId;
            NeighborhoodObject.NeighborhoodName = neighborhoodUpdateDto.NeighborhoodName;

            if (result)
            {
                return await Task.FromResult(-2);
            }
            else
            {
                _customerAddressContext.Neighborhoods.Update(NeighborhoodObject);
                return await _customerAddressContext.SaveChangesAsync();
            }

        }
    }
}
