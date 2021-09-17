using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAddress.Business.Dtos.Employee
{
    public class EmployeeListDto
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeSurname { get; set; }
        public int CompanyBranchId { get; set; }
    }
}
