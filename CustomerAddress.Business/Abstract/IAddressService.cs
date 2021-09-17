using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerAddress.Business.Dtos.Address;

namespace CustomerAddress.Business.Abstract
{
    public interface IAddressService
    {
        public Task<List<AddressListDto>> GetAddresses();
        public Task<AddressListDto> GetAddressesById(int id);
        //public Task<List<AddressListDto>> GetAddressesByCustomerId(int id);

        public Task<int> GetAddressIdByAll(AddressAddDto addressAddDto);
        public Task<List<AddressListDto>> GetAddressesByStreetId(int id);
        public Task<List<AddressListDto>> GetAddressesByTownId(int id);
        public Task<List<AddressListDto>> GetAddressesByCityId(int id);
        public Task<List<AddressListDto>> GetAddressesByNeighborhoodId(int id);

        /// ogrenci ekleme servisi
        /// </summary>
        public Task<int> AddAddress(AddressAddDto addressAddDto);

        /// <summary>
        /// ogrenci guncelleme servisi
        /// </summary>
        public Task<int> UpdateAddress(int id, AddressUpdateDto addressUpdateDto);

        /// <summary>
        /// ogrenci silme servisi
        /// </summary>
        public Task<int> DeleteAddress(int id);
    }
}
