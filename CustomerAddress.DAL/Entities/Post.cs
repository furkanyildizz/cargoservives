using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerAddress.Core.Entities;

namespace CustomerAddress.DAL.Entities
{
    public class Post : Audit, ISoftDeleted, IEntity
    {
        public int Id { get; set; }

        public DateTime? ShippingStartDate { get; set; }
        public DateTime? ShippingFinishDate { get; set; }
        public string PostCode { get; set; }
        public byte PostStatus { get; set; }



        public  ICollection<RouteMap> RouteMaps { get; set; }
        public int SenderId { get; set; }
        public Customer SenderFK { get; set; }

        public int ReceiverId { get; set; }
        public Customer ReceiverFK { get; set; }


        public int CompanyBranchId { get; set; }
        public CompanyBranch CompanyBranchFK { get; set; }


        public int EmployeeId { get; set; }
        public Employee EmployeeFK { get; set; }


        public bool IsDeleted { get; set; }

    }
}
