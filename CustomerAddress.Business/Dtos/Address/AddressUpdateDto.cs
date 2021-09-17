using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAddress.Business.Dtos.Address
{
    public class AddressUpdateDto
    {
        public int CityId { get; set; }
        public int TownId { get; set; }
        public int NeighborhoodId { get; set; }
        public int StreetId { get; set; }



        public string Title { get; set; }
        public string Description { get; set; }
    }
}
