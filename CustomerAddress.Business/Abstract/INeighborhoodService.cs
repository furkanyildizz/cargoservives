using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerAddress.Business.Dtos.Neighborhood;

namespace CustomerAddress.Business.Abstract
{
    public interface INeighborhoodService
    {
        public Task<List<NeighborhoodListDto>> GetNeighborhoods();
        public Task<NeighborhoodListDto> GetNeighborhoodsById(int id);
        public Task<List<NeighborhoodListDto>> GetNeighborhoodsByDistrictId(int id);


        public bool AnyNeighborhoodId(int id);

        public Task<bool> AnyNeighborhood(NeighborhoodAddDto neighborhoodAddDto);
        /// ogrenci ekleme servisi
        /// </summary>
        public Task<int> AddNeighborhood(NeighborhoodAddDto neighborhoodAddDto);


        public bool AnyTownId(int townId);
        /// <summary>
        /// ogrenci guncelleme servisi
        /// </summary>
        public Task<int> UpdateNeighborhood(int id, NeighborhoodUpdateDto neighborhoodUpdateDto);

        /// <summary>
        /// ogrenci silme servisi
        /// </summary>
        public Task<int> DeleteNeighborhood(int id);

    }
}
