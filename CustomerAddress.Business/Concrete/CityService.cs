using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerAddress.Business.Abstract;
using CustomerAddress.Business.Dtos.City;
using CustomerAddress.DAL.Context;
using CustomerAddress.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerAddress.Business.Concrete
{
    public class CityService : ICityService
    {
        private readonly CargoDbContext _customerAddressContext;
        public CityService(CargoDbContext customerAddressContext)
        {
            _customerAddressContext = customerAddressContext;
        }
        public async Task<int> AddCity(CityAddDto CityAddDto)
        {

            var result = await AnyCity(CityAddDto.CityName);

            if (result)
            {
                return await Task.FromResult(-2);
            }
            else
            {
                var newCity = new City
                {
                    CityName = CityAddDto.CityName,
                    CityRegion = CityAddDto.CityRegion

                };
                _customerAddressContext.Cities.Add(newCity);
                await _customerAddressContext.SaveChangesAsync();
                return await Task.FromResult(1);
            }


        }

        public async Task<int> DeleteCity(int id)
        {
            var result = await _customerAddressContext.Towns.Where(p => !p.IsDeleted).AnyAsync(p => p.CityId == id);


            if (result)
            {
                return await Task.FromResult(-2);

            }
            else
            {
                var CityObject = _customerAddressContext.Cities.SingleOrDefault(p => !p.IsDeleted && p.Id == id);
                if (CityObject != null)
                {
                    CityObject.IsDeleted = true;
                    _customerAddressContext.Cities.Update(CityObject);
                    return await _customerAddressContext.SaveChangesAsync();
                }

                else
                {
                    return await Task.FromResult(-1);
                }

            }


        }

        public bool AnyCityIds(int cityId)
        {
            return _customerAddressContext.Cities.Where(p => !p.IsDeleted).Any(p => p.Id == cityId);
        }

        public Task<List<CityListDto>> GetCities()
        {
            return _customerAddressContext.Cities.Where(p => !p.IsDeleted).Select(p => new CityListDto
            {
                Id = p.Id,
                CityName = p.CityName,
                CityRegion = p.CityRegion

            }).ToListAsync();
        }

        public Task<CityListDto> GetCitiesById(int id)
        {
            return _customerAddressContext.Cities.Where(p => !p.IsDeleted)
           .Select(p => new CityListDto
           {
               Id = p.Id,
               CityName = p.CityName,
               CityRegion = p.CityRegion

           }).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<int> UpdateCity(int id, CityUpdateDto cityUpdateDto)
        {
            var result = await AnyCityUpdate(cityUpdateDto.CityName, id);

            if (result)
            {
                return await Task.FromResult(-2);
            }
            else
            {
                var CityObject = await _customerAddressContext.Cities.SingleOrDefaultAsync(p => !p.IsDeleted && p.Id == id);

                if (CityObject == null)
                {
                    return await Task.FromResult(-1);
                }

                CityObject.CityName = cityUpdateDto.CityName;
                CityObject.CityRegion = cityUpdateDto.CityRegion;



                _customerAddressContext.Cities.Update(CityObject);
                return await _customerAddressContext.SaveChangesAsync();
            }

        }

        public async Task<bool> AnyCity(string CityName)
        {
            return await _customerAddressContext.Cities.Where(p => !p.IsDeleted).AnyAsync(p => p.CityName == CityName);

        }
        public async Task<bool> AnyCityUpdate(string CityName, int id)
        {

            var CityNames = _customerAddressContext.Cities.Where(p => !p.IsDeleted && p.Id != id).Select(p => p.CityName).ToList();
            if (CityNames.Contains(CityName))
            {
                return await Task.FromResult(true);
            }
            else
            {
                return await Task.FromResult(false);
            }


        }
    }
}
