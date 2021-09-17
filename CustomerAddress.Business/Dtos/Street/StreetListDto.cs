using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAddress.Business.Dtos.Street
{
    public class StreetListDto
    {
        public int Id { get; set; }
        public string StreetName { get; set; }
        public string NeighborhoodName { get; set; }
    }
}
