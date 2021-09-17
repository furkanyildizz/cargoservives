using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerAddress.Business.Dtos.City;

namespace CustomerAddress.Business.Abstract
{
    public interface ICityService
    {

        public bool AnyCityIds(int cityId);
        public Task<List<CityListDto>> GetCities();
        public Task<CityListDto> GetCitiesById(int id);

        public Task<bool> AnyCity(string CityName);
        /// </summary>
        public Task<int> AddCity(CityAddDto CityAddDto);

        /// <summary>
        /// ogrenci guncelleme servisi
        /// </summary>
        public Task<int> UpdateCity(int id, CityUpdateDto cityUpdateDto);

        /// <summary>
        /// ogrenci silme servisi
        /// </summary>
        public Task<int> DeleteCity(int id);

    }
}
