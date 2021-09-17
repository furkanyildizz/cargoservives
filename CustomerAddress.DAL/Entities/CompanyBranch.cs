using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerAddress.Core.Entities;

namespace CustomerAddress.DAL.Entities
{
    public class CompanyBranch : Audit, ISoftDeleted, IEntity
    {
        public int Id { get; set; }
        public string BranchName { get; set; }

        public int AddressId { get; set; }
        public Address AddressFK { get; set; }


        public ICollection<Employee> Employees { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<RouteMap> RouteMaps { get; set; }

        public bool IsDeleted { get; set; }
    }
}
