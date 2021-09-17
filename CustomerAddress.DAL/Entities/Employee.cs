using System.Collections.Generic;
using CustomerAddress.Core.Entities;

namespace CustomerAddress.DAL.Entities
{
    public class Employee : Audit, ISoftDeleted, IEntity
    {
        public int Id { get; set; }

        public string EmployeeName { get; set; }
        public string EmployeeSurname { get; set; }

        public int CompanyBranchId { get; set; }
        public CompanyBranch CompanyBranchFK { get; set; }

        public ICollection<Post> Posts { get; set; }
        public bool IsDeleted { get; set; }

    }
}
