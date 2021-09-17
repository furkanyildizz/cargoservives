using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAddress.Business.Dtos.RouteMap
{
    public class RouteMapListDto
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int CompanyBranchId { get; set; }
        public int StatusId { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
