using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAddress.Business.Dtos.Customer
{
    public class CustomerUpdateDto
    {
        public int AddressId { get; set; }
        public bool CustomerType { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public string TelNumber { get; set; }
        public string Mail { get; set; }
    }
}
