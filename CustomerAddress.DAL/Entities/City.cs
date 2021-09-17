using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerAddress.Core.Entities;

namespace CustomerAddress.DAL.Entities
{
    public class City : Audit, ISoftDeleted, IEntity
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public string CityRegion { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<Address> Addresses { get; set; }
        public ICollection<Town> Towns { get; set; }

    }
}
