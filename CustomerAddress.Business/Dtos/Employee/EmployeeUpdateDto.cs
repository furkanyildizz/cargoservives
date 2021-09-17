using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAddress.Business.Dtos.Employee
{
    public class EmployeeUpdateDto : IEquatable<EmployeeUpdateDto>
    {
        public string EmployeeName { get; set; }
        public string EmployeeSurname { get; set; }
        public int CompanyBranchId { get; set; }

        public bool Equals(EmployeeUpdateDto other)
        {
            return EmployeeName == other.EmployeeName && EmployeeSurname == other.EmployeeSurname && CompanyBranchId == other.CompanyBranchId;
        }
    }
}
