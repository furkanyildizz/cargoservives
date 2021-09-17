using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerAddress.Business.Dtos.District;

namespace CustomerAddress.Business.Abstract
{
    public interface ITownService
    {
        public Task<List<TownListDto>> GetTowns();
        public Task<TownListDto> GetTownsById(int id);
        public Task<List<TownListDto>> GetTownsByCityId(int id);

        public Task<bool> AnyTown(TownAddDto townAddDto);
        /// ogrenci ekleme servisi
        /// </summary>
        public Task<int> AddTown(TownAddDto townAddDto);
        public bool AndCityId(int cityId);

        /// <summary>
        /// ogrenci guncelleme servisi
        /// </summary>
        public Task<int> UpdateTown(int id, TownUpdateDto townUpdateDto);

        public bool AndTownId(int townId);
        /// <summary>
        /// ogrenci silme servisi
        /// </summary>
        public Task<int> DeleteTown(int id);
    }
}
