using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerAddress.Core.Entities;

namespace CustomerAddress.DAL.Entities
{
    public class Town : Audit, ISoftDeleted, IEntity
    {
        public int Id { get; set; }
        
        public string TownName { get; set; }
        public bool IsDeleted { get ; set; }

        public int CityId { get; set; }
        public City CityFK { get; set; }

        public ICollection<Address> Addresses { get; set; }
        public ICollection<Neighborhood> Neighborhoods { get; set; }
    }       
}
