using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerAddress.Business.Abstract;
using CustomerAddress.Business.Dtos.Address;
using CustomerAddress.DAL.Context;
using CustomerAddress.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerAddress.Business.Concrete
{
    public class AddressService : IAddressService
    {
        private readonly CargoDbContext _customerAddressContext;
        public AddressService(CargoDbContext customerAddressContext)
        {
            _customerAddressContext = customerAddressContext;
        }
        public async Task<int> AddAddress(AddressAddDto addressAddDto)
        {
            var result = await _customerAddressContext.Addresses.Where(p => !p.IsDeleted).AnyAsync(p => p.Description == addressAddDto.Description && p.StreetId == addressAddDto.StreetId);
            if (result)
            {
                return await Task.FromResult(-2);
            }
            else
            {
                var newAddress = new Address
                {
                    CityId=addressAddDto.CityId,
                    TownId=addressAddDto.TownId,
                    NeighborhoodId=addressAddDto.NeighborhoodId,
                    StreetId = addressAddDto.StreetId,
                    //CustomerId = addressAddDto.CustomerId,
                    Title = addressAddDto.Title,
                    Description = addressAddDto.Description

                };
                _customerAddressContext.Addresses.Add(newAddress);
                return await _customerAddressContext.SaveChangesAsync();
            }



        }

        public async Task<int> DeleteAddress(int id)
        {
            var AddressObject = _customerAddressContext.Addresses.SingleOrDefault(p => !p.IsDeleted && p.Id == id);
            if (AddressObject != null)
            {
                AddressObject.IsDeleted = true;
                _customerAddressContext.Addresses.Update(AddressObject);
                return await _customerAddressContext.SaveChangesAsync();
            }
            else
            {
                return await Task.FromResult(-1);
            }
        }

        public Task<List<AddressListDto>> GetAddresses()
        {

            return _customerAddressContext.Addresses.Include(p => p.StreetFK).Where(p => !p.IsDeleted).Include(p => p.StreetFK.NeighborhoodFK).Include(p => p.StreetFK.NeighborhoodFK.TownFK.CityFK).Select(p => new AddressListDto
            {

                Id = p.Id,
                CityName = _customerAddressContext.Cities.Where(q => !q.IsDeleted && p.StreetFK.NeighborhoodFK.TownFK.CityId == q.Id).Select(q => q.CityName).FirstOrDefault(),
                TownName = _customerAddressContext.Towns.Where(q => !q.IsDeleted && p.StreetFK.NeighborhoodFK.TownId == q.Id).Select(q => q.TownName).FirstOrDefault(),
                NeighborhoodName = _customerAddressContext.Neighborhoods.Where(q => !q.IsDeleted && p.StreetFK.NeighborhoodId == q.Id).Select(q => q.NeighborhoodName).FirstOrDefault(),
                StreetName = p.StreetFK.StreetName,
                Title = p.Title,
                Description = p.Description

            }).ToListAsync();
        }

        //public async Task<List<AddressListDto>> GetAddressesByCustomerId(int id)
        //{
        //    return await _customerAddressContext.Addresses.Where(p => !p.IsDeleted && p.CustomerId == id).Include(p => p.CustomerFK).Include(p => p.StreetFK).Include(p => p.StreetFK.NeighborhoodFK).Include(p => p.StreetFK.NeighborhoodFK.DistrictFK.CityFK)
        //        .Select(p => new AddressListDto
        //        {
        //            Id = p.Id,
        //            CityName = _customerAddressContext.Cities.Where(q => !q.IsDeleted && p.StreetFK.NeighborhoodFK.DistrictFK.CityId == q.Id).Select(q => q.CityName).FirstOrDefault(),
        //            DistrictName = _customerAddressContext.Districts.Where(q => !q.IsDeleted && p.StreetFK.NeighborhoodFK.DistrictId == q.Id).Select(q => q.DistrictName).FirstOrDefault(),
        //            NeighborhoodName = _customerAddressContext.Neighborhoods.Where(q => !q.IsDeleted && p.StreetFK.NeighborhoodId == q.Id).Select(q => q.NeighborhoodName).FirstOrDefault(),
        //            StreetName = p.StreetFK.StreetName,
        //            CustomerName = p.CustomerFK.CustomerName,
        //            CustomerSurname = p.CustomerFK.CustomerSurname,
        //            Title = p.Title,
        //            Description = p.Description

        //        }).ToListAsync();
        //}

        public async Task<List<AddressListDto>> GetAddressesByStreetId(int id)
        {
            return await _customerAddressContext.Addresses.Where(p => !p.IsDeleted && p.StreetId == id).Include(p => p.StreetFK).Include(p => p.StreetFK.NeighborhoodFK).Include(p => p.StreetFK.NeighborhoodFK.TownFK.CityFK)
                .Select(p => new AddressListDto
                {
                    Id = p.Id,
                    CityName = _customerAddressContext.Cities.Where(q => !q.IsDeleted && p.StreetFK.NeighborhoodFK.TownFK.CityId == q.Id).Select(q => q.CityName).FirstOrDefault(),
                    TownName = _customerAddressContext.Towns.Where(q => !q.IsDeleted && p.StreetFK.NeighborhoodFK.TownId == q.Id).Select(q => q.TownName).FirstOrDefault(),
                    NeighborhoodName = _customerAddressContext.Neighborhoods.Where(q => !q.IsDeleted && p.StreetFK.NeighborhoodId == q.Id).Select(q => q.NeighborhoodName).FirstOrDefault(),
                    StreetName = p.StreetFK.StreetName,
                    Title = p.Title,
                    Description = p.Description

                }).ToListAsync();
        }

        public Task<AddressListDto> GetAddressesById(int id)
        {
            return _customerAddressContext.Addresses.Where(p => !p.IsDeleted).Include(p => p.StreetFK).Include(p => p.StreetFK.NeighborhoodFK).Include(p => p.StreetFK.NeighborhoodFK.TownFK.CityFK)
            .Select(p => new AddressListDto
            {
                Id = p.Id,
                CityName = _customerAddressContext.Cities.Where(q => !q.IsDeleted && p.StreetFK.NeighborhoodFK.TownFK.CityId == q.Id).Select(q => q.CityName).FirstOrDefault(),
                TownName = _customerAddressContext.Towns.Where(q => !q.IsDeleted && p.StreetFK.NeighborhoodFK.TownId == q.Id).Select(q => q.TownName).FirstOrDefault(),
                NeighborhoodName = _customerAddressContext.Neighborhoods.Where(q => !q.IsDeleted && p.StreetFK.NeighborhoodId == q.Id).Select(q => q.NeighborhoodName).FirstOrDefault(),
                StreetName = p.StreetFK.StreetName,
                Title = p.Title,
                Description = p.Description

            }).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<int> UpdateAddress(int id, AddressUpdateDto addressUpdateDto)
        {
            var result = await _customerAddressContext.Addresses.Where(p => !p.IsDeleted && p.Id != id).AnyAsync(p => p.Description == addressUpdateDto.Description && p.StreetId == addressUpdateDto.StreetId);

            var AddressObject = await _customerAddressContext.Addresses.SingleOrDefaultAsync(p => !p.IsDeleted && p.Id == id);

            if (AddressObject == null)
            {
                return await Task.FromResult(-1);
            }
            else if (result)
            {
                return await Task.FromResult(-2);
            }
            else
            {

                AddressObject.CityId = addressUpdateDto.CityId;
                AddressObject.TownId = addressUpdateDto.TownId;
                AddressObject.NeighborhoodId = addressUpdateDto.NeighborhoodId;
                AddressObject.StreetId = addressUpdateDto.StreetId;
                AddressObject.Title = addressUpdateDto.Title;
                AddressObject.Description = addressUpdateDto.Description;


                _customerAddressContext.Addresses.Update(AddressObject);
                return await _customerAddressContext.SaveChangesAsync();
            }
        }

        public async Task<List<AddressListDto>> GetAddressesByTownId(int id)
        {
            return await _customerAddressContext.Addresses.Where(p => !p.IsDeleted && p.StreetFK.NeighborhoodFK.TownId == id).Include(p => p.StreetFK).Include(p => p.StreetFK.NeighborhoodFK).Include(p => p.StreetFK.NeighborhoodFK.TownFK.CityFK)
               .Select(p => new AddressListDto
               {
                   Id = p.Id,
                   CityName = _customerAddressContext.Cities.Where(q => !q.IsDeleted && p.StreetFK.NeighborhoodFK.TownFK.CityId == q.Id).Select(q => q.CityName).FirstOrDefault(),
                   TownName = _customerAddressContext.Towns.Where(q => !q.IsDeleted && p.StreetFK.NeighborhoodFK.TownId == q.Id).Select(q => q.TownName).FirstOrDefault(),
                   NeighborhoodName = _customerAddressContext.Neighborhoods.Where(q => !q.IsDeleted && p.StreetFK.NeighborhoodId == q.Id).Select(q => q.NeighborhoodName).FirstOrDefault(),
                   StreetName = p.StreetFK.StreetName,
                   Title = p.Title,
                   Description = p.Description

               }).ToListAsync();
        }


        public async Task<List<AddressListDto>> GetAddressesByCityId(int id)
        {
            return await _customerAddressContext.Addresses.Where(p => !p.IsDeleted && p.StreetFK.NeighborhoodFK.TownFK.CityId == id).Include(p => p.StreetFK).Include(p => p.StreetFK.NeighborhoodFK).Include(p => p.StreetFK.NeighborhoodFK.TownFK.CityFK)
               .Select(p => new AddressListDto
               {
                   Id = p.Id,
                   CityName = _customerAddressContext.Cities.Where(q => !q.IsDeleted && p.StreetFK.NeighborhoodFK.TownFK.CityId == q.Id).Select(q => q.CityName).FirstOrDefault(),
                   TownName = _customerAddressContext.Towns.Where(q => !q.IsDeleted && p.StreetFK.NeighborhoodFK.TownId == q.Id).Select(q => q.TownName).FirstOrDefault(),
                   NeighborhoodName = _customerAddressContext.Neighborhoods.Where(q => !q.IsDeleted && p.StreetFK.NeighborhoodId == q.Id).Select(q => q.NeighborhoodName).FirstOrDefault(),
                   StreetName = p.StreetFK.StreetName,
                   Title = p.Title,
                   Description = p.Description

               }).ToListAsync();
        }

        public async Task<int> GetAddressIdByAll(AddressAddDto addressAddDto)
        {
            return await _customerAddressContext.Addresses.Where(p => !p.IsDeleted && p.CityId == addressAddDto.CityId && p.TownId == addressAddDto.TownId && p.NeighborhoodId == addressAddDto.NeighborhoodId
            && p.StreetId == addressAddDto.StreetId && p.Title == addressAddDto.Title && p.Description == addressAddDto.Description).Select(p=>p.Id).FirstOrDefaultAsync();
        }

        public async Task<List<AddressListDto>> GetAddressesByNeighborhoodId(int id)
        {
            return await _customerAddressContext.Addresses.Where(p => !p.IsDeleted && p.StreetFK.NeighborhoodId == id).Include(p => p.StreetFK).Include(p => p.StreetFK.NeighborhoodFK).Include(p => p.StreetFK.NeighborhoodFK.TownFK.CityFK)
               .Select(p => new AddressListDto
               {
                   Id = p.Id,
                   CityName = _customerAddressContext.Cities.Where(q => !q.IsDeleted && p.StreetFK.NeighborhoodFK.TownFK.CityId == q.Id).Select(q => q.CityName).FirstOrDefault(),
                   TownName = _customerAddressContext.Towns.Where(q => !q.IsDeleted && p.StreetFK.NeighborhoodFK.TownId == q.Id).Select(q => q.TownName).FirstOrDefault(),
                   NeighborhoodName = _customerAddressContext.Neighborhoods.Where(q => !q.IsDeleted && p.StreetFK.NeighborhoodId == q.Id).Select(q => q.NeighborhoodName).FirstOrDefault(),
                   StreetName = p.StreetFK.StreetName,
                   Title = p.Title,
                   Description = p.Description

               }).ToListAsync();
        }

        //public Task<List<AddressListDto>> GetAddressesByCustomerId(int id)
        //{

        //    var adressId = _customerAddressContext.Customers.Where(p => !p.IsDeleted && p.Id == id).Select(p => p.AddressId);
        //    return _customerAddressContext.Addresses.Where(p => !p.IsDeleted && p.Id==adressId);
        //}
    }
}
