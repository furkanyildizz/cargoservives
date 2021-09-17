using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAddress.Business.Dtos.Post
{
    public class PostListDto
    {
        public int Id { get; set; }
        public string PostCode { get; set; }
        //public byte PostStatus { get; set; }
        public string PostStatus { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public int CompanyBranchId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime? ShippingStartDate { get; set; }
        public DateTime? ShippingFinishDate { get; set; }
    }
}
