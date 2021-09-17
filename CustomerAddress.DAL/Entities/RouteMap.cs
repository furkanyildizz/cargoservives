using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerAddress.Core.Entities;

namespace CustomerAddress.DAL.Entities
{
    public class RouteMap : Audit, ISoftDeleted, IEntity
    {
        public int Id { get; set; }
      
        public DateTime? CreatedDate { get; set; }
        public bool IsDeleted { get; set; }

        public int StatusId { get; set; }
        public Status StatusFK { get; set; }
        public int PostId { get; set; }
        public Post PostFK { get; set; }
        public int CompanyBranchId { get; set; }
        public CompanyBranch CompanyBranchFK { get; set; }
    }
}
