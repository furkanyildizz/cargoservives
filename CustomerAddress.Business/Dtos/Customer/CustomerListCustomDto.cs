using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAddress.Business.Dtos.Customer
{
    public class CustomerListCustomDto
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public string DistrictName { get; set; }
        public string NeighborhoodName { get; set; }
        public string StreetName { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public string TelNumber { get; set; }
        public string Mail { get; set; }
    }
}
