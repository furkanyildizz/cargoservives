using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerAddress.Core.Entities;

namespace CustomerAddress.DAL.Entities
{
    public class Neighborhood : Audit, ISoftDeleted, IEntity
    {
        public int Id { get; set; }
        public string NeighborhoodName { get; set; }
        public bool IsDeleted { get; set ; }

        public int TownId { get; set; }
        public Town TownFK { get; set; }

        public ICollection<Street> Streets { get; set; }
        public ICollection<Address> Addresses { get; set; }
    }
}
