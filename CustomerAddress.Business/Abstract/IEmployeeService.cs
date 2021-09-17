using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerAddress.Business.Dtos.Employee;

namespace CustomerAddress.Business.Abstract
{
    public interface IEmployeeService
    {
        public Task<List<EmployeeListDto>> GetEmployees();
        public Task<EmployeeListDto> GetEmployeesById(int id);


        /// </summary>
        public Task<int> AddEmployee(EmployeeAddDto EmployeeAddDto);
        public bool AnyCompanyBranchId(int companyBranchId);

        /// <summary>
        /// ogrenci guncelleme servisi
        /// </summary>
        public Task<int> UpdateEmployee(int id, EmployeeUpdateDto employeeUpdateDto);

        /// <summary>
        /// ogrenci silme servisi
        /// </summary>
        public Task<int> DeleteEmployee(int id);
    }
}
