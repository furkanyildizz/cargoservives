using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerAddress.Core.Entities;

namespace CustomerAddress.DAL.Entities
{
    public class Status : Audit, ISoftDeleted , IEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get ; set ; }

        public  ICollection<RouteMap> RouteMaps { get; set; }
    }
}
