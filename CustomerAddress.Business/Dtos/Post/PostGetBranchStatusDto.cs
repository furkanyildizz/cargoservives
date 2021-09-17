using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAddress.Business.Dtos.Post
{
    public class PostGetBranchStatusDto
    {
        public int BranchId { get; set; }
        public byte PostStatus { get; set; }
    }
}
