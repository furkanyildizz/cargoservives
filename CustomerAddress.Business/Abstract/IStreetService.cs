using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerAddress.Business.Dtos.Street;

namespace CustomerAddress.Business.Abstract
{
    public interface IStreetService
    {
        public Task<List<StreetListDto>> GetStreets();
        public Task<StreetListDto> GetStreetsById(int id);
        public Task<List<StreetListDto>> GetStreetsByNeighborhoodId(int id);

        public Task<bool> AnyStreet(StreetAddDto streetAddDto);
        public bool AnyStreetId(int StreetId);

        /// ogrenci ekleme servisi
        /// </summary>
        public Task<int> AddStreet(StreetAddDto streetAddDto);
        public bool AnyNeighborhood(int neighborhoodId);

        /// <summary>
        /// ogrenci guncelleme servisi
        /// </summary>
        public Task<int> UpdateStreet(int id, StreetUpdateDto streetUpdateDto);

        /// <summary>
        /// ogrenci silme servisi
        /// </summary>
        public Task<int> DeleteStreet(int id);

    }
}
