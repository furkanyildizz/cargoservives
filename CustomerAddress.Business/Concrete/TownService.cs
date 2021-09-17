using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerAddress.Business.Abstract;
using CustomerAddress.Business.Dtos.District;
using CustomerAddress.DAL.Context;
using CustomerAddress.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerAddress.Business.Concrete
{
    public class TownService : ITownService
    {
        private readonly CargoDbContext _customerAddressContext;
        public TownService(CargoDbContext customerAddressContext)
        {
            _customerAddressContext = customerAddressContext;
        }
        public async Task<int> AddTown(TownAddDto townAddDto)
        {
            var result = await AnyTown(townAddDto);
            if (result)
            {
                return await Task.FromResult(-2);
            }
            else
            {
                var newTown = new Town
                {
                    TownName = townAddDto.TownName,
                    CityId = townAddDto.CityId

                };
                _customerAddressContext.Towns.Add(newTown);
                await _customerAddressContext.SaveChangesAsync();
                return await Task.FromResult(1);

            }


        }

        public async Task<int> DeleteTown(int id)
        {
            var result = await _customerAddressContext.Neighborhoods.Where(p => !p.IsDeleted).AnyAsync(p => p.TownId == id);
            var TownObject = _customerAddressContext.Towns.SingleOrDefault(p => !p.IsDeleted && p.Id == id);
            if (result)
            {
                return await Task.FromResult(-2);
            }
            else
            {
                if (TownObject != null)
                {
                    TownObject.IsDeleted = true;
                    _customerAddressContext.Towns.Update(TownObject);
                    return await _customerAddressContext.SaveChangesAsync();
                }
                else
                {
                    return await Task.FromResult(-1);
                }
            }

        }

        public bool AndTownId(int townId)
        {
            return _customerAddressContext.Towns.Where(p => !p.IsDeleted).Any(p => p.Id == townId);
        }

        public bool AndCityId(int cityId)
        {
            return _customerAddressContext.Cities.Where(p => !p.IsDeleted).Any(p => p.Id == cityId);
        }

        public Task<List<TownListDto>> GetTowns()
        {
            return _customerAddressContext.Towns.Include(p => p.CityFK).Where(p => !p.IsDeleted).Select(p => new TownListDto
            {
                Id = p.Id,
                CityName = p.CityFK.CityName,
                TownName = p.TownName

            }).ToListAsync();
        }

        public async Task<List<TownListDto>> GetTownsByCityId(int id)
        {
            return await _customerAddressContext.Towns.Where(p => !p.IsDeleted && p.CityId == id).Include(p => p.CityFK)
                .Select(p => new TownListDto
                {
                    Id = p.Id,
                    CityName = p.CityFK.CityName,
                    TownName = p.TownName

                }).ToListAsync();
        }

        public Task<TownListDto> GetTownsById(int id)
        {
            return _customerAddressContext.Towns.Where(p => !p.IsDeleted).Include(p => p.CityFK)
           .Select(p => new TownListDto
           {
               Id = p.Id,
               CityName = p.CityFK.CityName,
               TownName = p.TownName

           }).FirstOrDefaultAsync(p => p.Id == id);
        }



        public async Task<bool> AnyTown(TownAddDto townAddDto)
        {
            string cityName = _customerAddressContext.Cities.Where(p => !p.IsDeleted && p.Id == townAddDto.CityId).Select(p => p.CityName).FirstOrDefault();
            var result = await _customerAddressContext.Towns.Where(p => !p.IsDeleted).Include(p => p.CityFK).AnyAsync(p => p.TownName == townAddDto.TownName && p.CityFK.CityName == cityName);

            return result;

        }

        public async Task<bool> AnyTownUpdate(TownUpdateDto townUpdateDto, int id)
        {
            string cityName = _customerAddressContext.Cities.Where(p => !p.IsDeleted && p.Id == townUpdateDto.CityId).Select(p => p.CityName).FirstOrDefault();
            var result = await _customerAddressContext.Towns.Where(p => !p.IsDeleted && p.Id != id).Include(p => p.CityFK).AnyAsync(p => p.TownName == townUpdateDto.TownName && p.CityFK.CityName == cityName);
            return result;
        }

        public async Task<int> UpdateTown(int id, TownUpdateDto townUpdateDto)
        {
            var TownObject = await _customerAddressContext.Towns.SingleOrDefaultAsync(p => !p.IsDeleted && p.Id == id);
            var result = await AnyTownUpdate(townUpdateDto, id);



            if (TownObject == null)
            {
                return await Task.FromResult(-1);
            }

            TownObject.CityId = townUpdateDto.CityId;
            TownObject.TownName = townUpdateDto.TownName;

            if (result)
            {
                return await Task.FromResult(-2);
            }
            else
            {
                _customerAddressContext.Towns.Update(TownObject);
                return await _customerAddressContext.SaveChangesAsync();
            }


        }
    }
}
