using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerAddress.Business.Abstract;
using CustomerAddress.Business.Dtos.Employee;
using CustomerAddress.DAL.Context;
using CustomerAddress.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerAddress.Business.Concrete
{
    public class EmployeeService : IEmployeeService
    {
        private readonly CargoDbContext _customerAddressContext;
        public EmployeeService(CargoDbContext customerAddressContext)
        {
            _customerAddressContext = customerAddressContext;
        }
        public async Task<int> AddEmployee(EmployeeAddDto EmployeeAddDto)
        {
            var result = await AnyEmployee(EmployeeAddDto);

            if (result)
            {
                return await Task.FromResult(-2);
            }
            else
            {
                var newEmployee = new Employee
                {
                    EmployeeName = EmployeeAddDto.EmployeeName,
                    EmployeeSurname = EmployeeAddDto.EmployeeSurname,
                    CompanyBranchId = EmployeeAddDto.CompanyBranchId

                };
                _customerAddressContext.Employees.Add(newEmployee);
                await _customerAddressContext.SaveChangesAsync();
                return await Task.FromResult(1);
            }

        }

        public bool AnyCompanyBranchId(int companyBranchId)
        {
            return _customerAddressContext.CompanyBranches.Where(p => !p.IsDeleted).Any(p => p.Id == companyBranchId);
        }

        public async Task<bool> AnyEmployee(EmployeeAddDto Employee)
        {
            return await _customerAddressContext.Employees.Where(p => !p.IsDeleted)
                .AnyAsync(p => p.EmployeeName == Employee.EmployeeName&&p.EmployeeSurname==Employee.EmployeeSurname&&p.CompanyBranchId==Employee.CompanyBranchId);

        }

        public async Task<int> DeleteEmployee(int id)
        {
            var result = await _customerAddressContext.Posts.Where(p=>!p.IsDeleted).AnyAsync(p => p.EmployeeId == id);


            if (result)
            {
                return await Task.FromResult(-2);

            }
            else
            {
                var EmployeeObject = _customerAddressContext.Employees.SingleOrDefault(p => !p.IsDeleted && p.Id == id);
                if (EmployeeObject != null)
                {
                    EmployeeObject.IsDeleted = true;
                    _customerAddressContext.Employees.Update(EmployeeObject);
                    return await _customerAddressContext.SaveChangesAsync();
                }

                else
                {
                    return await Task.FromResult(-1);
                }

            }
        }

        public Task<List<EmployeeListDto>> GetEmployees()
        {
            return _customerAddressContext.Employees.Where(p => !p.IsDeleted).Select(p => new EmployeeListDto
            {
                Id = p.Id,
                EmployeeName = p.EmployeeName,
                EmployeeSurname = p.EmployeeSurname,
                CompanyBranchId = p.CompanyBranchId,

            }).ToListAsync();
        }

        public Task<EmployeeListDto> GetEmployeesById(int id)
        {
            return _customerAddressContext.Employees.Where(p => !p.IsDeleted)
           .Select(p => new EmployeeListDto
           {
               Id = p.Id,
               EmployeeName = p.EmployeeName,
               EmployeeSurname = p.EmployeeSurname,
               CompanyBranchId = p.CompanyBranchId,

           }).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<int> UpdateEmployee(int id, EmployeeUpdateDto employeeUpdateDto)
        {
            var result = await AnyEmployeeUpdate(employeeUpdateDto, id);

            if (result)
            {
                return await Task.FromResult(-2);
            }
            else
            {
                var EmployeeObject = await _customerAddressContext.Employees.SingleOrDefaultAsync(p => !p.IsDeleted && p.Id == id);

                if (EmployeeObject == null)
                {
                    return await Task.FromResult(-1);
                }

                EmployeeObject.EmployeeName = employeeUpdateDto.EmployeeName;
                EmployeeObject.EmployeeSurname = employeeUpdateDto.EmployeeSurname;
                EmployeeObject.CompanyBranchId = employeeUpdateDto.CompanyBranchId;



                _customerAddressContext.Employees.Update(EmployeeObject);
                return await _customerAddressContext.SaveChangesAsync();
            }
        }

        public async Task<bool> AnyEmployeeUpdate(EmployeeUpdateDto employeeUpdateDto, int id)
        {

            var employees =  _customerAddressContext.Employees.Where(p => !p.IsDeleted && p.Id != id).Select(p => new EmployeeUpdateDto
            {
                EmployeeName = p.EmployeeName,
                EmployeeSurname = p.EmployeeSurname,
                CompanyBranchId = p.CompanyBranchId

            }).ToList();


            if (employees.Contains(employeeUpdateDto))
            {
                return await Task.FromResult(true);
            }
            else
            {
                return await Task.FromResult(false);
            }

            //int sayac = 0;
            //foreach (var item in employees)
            //{

            //    if (item.CompanyBranchId == employeeUpdateDto.CompanyBranchId && item.EmployeeName == employeeUpdateDto.EmployeeName && item.EmployeeSurname == employeeUpdateDto.EmployeeSurname)
            //    {
            //        sayac++;
            //        return await Task.FromResult(true);

            //    }

            //}

            //if (sayac == 0)
            //{
            //    return await Task.FromResult(false);
            //}
            //else
            //{
            //    return await Task.FromResult(false);
            //}





        }
    }
}
