using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAddress.Business.Dtos.CompanyBranch
{
    public class CompanyBranchListDto
    {
        public int Id { get; set; }
        public string BranchName { get; set; }

        public int AddressId { get; set; }
    }
}
