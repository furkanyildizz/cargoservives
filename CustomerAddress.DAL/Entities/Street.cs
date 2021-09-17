using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerAddress.Core.Entities;

namespace CustomerAddress.DAL.Entities
{
    public class Street : Audit, ISoftDeleted, IEntity
    {
        public int Id { get; set; }
        public string StreetName { get; set; }
        public bool IsDeleted { get; set; }

        public int NeighborhoodId { get; set; }
        public Neighborhood NeighborhoodFK { get; set; }

        public ICollection<Address> Addresses { get; set; }
    }
}
