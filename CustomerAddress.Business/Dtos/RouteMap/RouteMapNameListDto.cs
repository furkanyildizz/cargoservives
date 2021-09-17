using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAddress.Business.Dtos.RouteMap
{
    public class RouteMapNameListDto
    {
        public int Id { get; set; }
        public string PostCode { get; set; }
        public string CompanyBranchName { get; set; }
        public string StatusDescription{ get; set; }
        public DateTime? CreatedDate { get; set; }

    }
}
