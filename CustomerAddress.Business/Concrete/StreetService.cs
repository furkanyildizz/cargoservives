using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerAddress.Business.Abstract;
using CustomerAddress.Business.Dtos.Street;
using CustomerAddress.DAL.Context;
using CustomerAddress.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerAddress.Business.Concrete
{
    public class StreetService : IStreetService
    {
        private readonly CargoDbContext _customerAddressContext;
        public StreetService(CargoDbContext customerAddressContext)
        {
            _customerAddressContext = customerAddressContext;
        }


        public async Task<int> AddStreet(StreetAddDto streetAddDto)
        {
            var result = await AnyStreet(streetAddDto);
            if (result)
            {
                return await Task.FromResult(-2);
            }
            else
            {
                var newStreet = new Street
                {
                    StreetName = streetAddDto.StreetName,
                    NeighborhoodId = streetAddDto.NeighborhoodId

                };
                _customerAddressContext.Streets.Add(newStreet);
                await _customerAddressContext.SaveChangesAsync();
                return await Task.FromResult(1);

            }
        }

        public bool AnyNeighborhood(int neighborhoodId)
        {
            return _customerAddressContext.Neighborhoods.Where(p => !p.IsDeleted).Any(p => p.Id == neighborhoodId);
        }

        public async Task<bool> AnyStreet(StreetAddDto streetAddDto)
        {
            return await _customerAddressContext.Streets.Where(p => !p.IsDeleted ).AnyAsync(p => p.StreetName == streetAddDto.StreetName&p.NeighborhoodId== streetAddDto.NeighborhoodId);
        }

        public bool AnyStreetId(int StreetId)
        {
            return _customerAddressContext.Streets.Where(p => !p.IsDeleted).Any(p => p.Id == StreetId);
        }




        public async Task<int> DeleteStreet(int id)
        {
            var result = await _customerAddressContext.Addresses.Where(p => !p.IsDeleted).AnyAsync(p => p.StreetId == id);
            var StreetObject = _customerAddressContext.Streets.SingleOrDefault(p => !p.IsDeleted && p.Id == id);
            if (result)
            {
                return await Task.FromResult(-2);
            }
            else
            {
                if (StreetObject != null)
                {
                    StreetObject.IsDeleted = true;
                    _customerAddressContext.Streets.Update(StreetObject);
                    return await _customerAddressContext.SaveChangesAsync();
                }
                else
                {
                    return await Task.FromResult(-1);
                }
            }
        }

        public Task<List<StreetListDto>> GetStreets()
        {
            return _customerAddressContext.Streets.Include(p => p.NeighborhoodFK).Where(p => !p.IsDeleted).Select(p => new StreetListDto
            {
                Id = p.Id,
                NeighborhoodName = p.NeighborhoodFK.NeighborhoodName,
                StreetName = p.StreetName


            }).ToListAsync();
        }

        public Task<StreetListDto> GetStreetsById(int id)
        {
            return _customerAddressContext.Streets.Where(p => !p.IsDeleted).Include(p => p.NeighborhoodFK)
          .Select(p => new StreetListDto
          {
              Id = p.Id,
              StreetName = p.StreetName,
              NeighborhoodName = p.NeighborhoodFK.NeighborhoodName

          }).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<StreetListDto>> GetStreetsByNeighborhoodId(int id)
        {
            return await _customerAddressContext.Streets.Where(p => !p.IsDeleted && p.NeighborhoodId == id).Include(p => p.NeighborhoodFK)
                .Select(p => new StreetListDto
                {
                    Id = p.Id,
                    NeighborhoodName = p.NeighborhoodFK.NeighborhoodName,
                    StreetName = p.StreetName

                }).ToListAsync();
        }


        public async Task<bool> AnyStreetUpdate(StreetUpdateDto streetUpdateDto, int id)
        {
            string neighborhoodName = _customerAddressContext.Neighborhoods.Where(p => !p.IsDeleted && p.Id == streetUpdateDto.NeighborhoodId).Select(p => p.NeighborhoodName).FirstOrDefault();
            var result = await _customerAddressContext.Streets.Where(p => !p.IsDeleted && p.Id != id).Include(p => p.NeighborhoodFK).AnyAsync(p => p.StreetName == streetUpdateDto.StreetName && p.NeighborhoodFK.NeighborhoodName == neighborhoodName);
            return result;
        }

        public async Task<int> UpdateStreet(int id, StreetUpdateDto streetUpdateDto)
        {
            var StreetObject = await _customerAddressContext.Streets.SingleOrDefaultAsync(p => !p.IsDeleted && p.Id == id);
            var result = await AnyStreetUpdate(streetUpdateDto, id);



            if (StreetObject == null)
            {
                return await Task.FromResult(-1);
            }

            StreetObject.NeighborhoodId = streetUpdateDto.NeighborhoodId;
            StreetObject.StreetName = streetUpdateDto.StreetName;

            if (result)
            {
                return await Task.FromResult(-2);
            }
            else
            {
                _customerAddressContext.Streets.Update(StreetObject);
                return await _customerAddressContext.SaveChangesAsync();
            }
        }
    }
}
