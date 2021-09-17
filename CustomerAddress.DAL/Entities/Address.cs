using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerAddress.Core.Entities;

namespace CustomerAddress.DAL.Entities
{
    /// <summary>
    /// ///////////////////OLDUMU ACAVA
    /// </summary>
    public class Address :Audit , ISoftDeleted, IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public int StreetId { get; set; }
        public Street StreetFK { get; set; }

        public int CityId { get; set; }
        public City CityFK { get; set; }

        public int TownId { get; set; }
        public Town TownFK { get; set; }

        public int NeighborhoodId { get; set; }
        public Neighborhood NeighborhoodFK { get; set; }


        public ICollection<Customer> Customers { get; set; }
        public ICollection<CompanyBranch> CompanyBranches { get; set; }

        public bool IsDeleted { get; set ; }
    }
}