using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAddress.Business.Dtos.Address
{
    public class AddressListDto
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public string TownName { get; set; }
        public string NeighborhoodName { get; set; }
        public string StreetName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
