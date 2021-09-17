using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerAddress.Business.Abstract;
using CustomerAddress.Business.Dtos.CompanyBranch;
using CustomerAddress.DAL.Context;
using CustomerAddress.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerAddress.Business.Concrete
{
    public class CompanyBranchService : ICompanyBranchService
    {
        private readonly CargoDbContext _customerAddressContext;
        public CompanyBranchService(CargoDbContext customerAddressContext)
        {
            _customerAddressContext = customerAddressContext;
        }
        public async Task<int> AddCompanyBranch(CompanyBranchAddDto CompanyBranchAddDto)
        {

            var result = await AnyCompanyBranch(CompanyBranchAddDto.BranchName);

            if (result)
            {
                return await Task.FromResult(-2);
            }
            else
            {
                var newCompanyBranch= new CompanyBranch
                {
                    BranchName = CompanyBranchAddDto.BranchName,
                    AddressId = CompanyBranchAddDto.AddressId

                };
                _customerAddressContext.CompanyBranches.Add(newCompanyBranch);
                await _customerAddressContext.SaveChangesAsync();
                return await Task.FromResult(1);
            }
        }

        public bool AnyAddressId(int addressId)
        {
            return _customerAddressContext.Addresses.Where(p => !p.IsDeleted).Any(p => p.Id == addressId);
        }

        public async Task<bool> AnyCompanyBranch(string CompanyBranchName)
        {
            return await _customerAddressContext.CompanyBranches.Where(p => !p.IsDeleted).AnyAsync(p=>p.BranchName==CompanyBranchName);

        }

        public async Task<int> DeleteCompanyBranch(int id)
        {
            var result = await _customerAddressContext.Employees.Where(p => !p.IsDeleted).AnyAsync(p => p.CompanyBranchId == id);


            if (result)
            {
                return await Task.FromResult(-2);

            }
            else
            {
                var CompanyBranchObject = _customerAddressContext.CompanyBranches.SingleOrDefault(p => !p.IsDeleted && p.Id == id);
                if (CompanyBranchObject != null)
                {
                    CompanyBranchObject.IsDeleted = true;
                    _customerAddressContext.CompanyBranches.Update(CompanyBranchObject);
                    return await _customerAddressContext.SaveChangesAsync();
                }

                else
                {
                    return await Task.FromResult(-1);
                }

            }
        }

        public Task<List<CompanyBranchListDto>> GetCompanyBranches()
        {
            return _customerAddressContext.CompanyBranches.Where(p => !p.IsDeleted).Select(p => new CompanyBranchListDto
            {
                Id = p.Id,
                BranchName = p.BranchName,
                AddressId = p.AddressId

            }).ToListAsync();
        }

        public Task<CompanyBranchListDto> GetCompanyBranchesById(int id)
        {
            return _customerAddressContext.CompanyBranches.Where(p => !p.IsDeleted)
           .Select(p => new CompanyBranchListDto
           {
               Id = p.Id,
               BranchName = p.BranchName,
               AddressId = p.AddressId

           }).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<int> UpdateCompanyBranch(int id, CompanyBranchUpdateDto companyBranchUpdateDto)
        {
            var result = await AnyCompanyBranchUpdate(companyBranchUpdateDto.BranchName, id);

            if (result)
            {
                return await Task.FromResult(-2);
            }
            else
            {
                var CompanyBranchObject = await _customerAddressContext.CompanyBranches.SingleOrDefaultAsync(p => !p.IsDeleted && p.Id == id);

                if (CompanyBranchObject == null)
                {
                    return await Task.FromResult(-1);
                }

                CompanyBranchObject.BranchName = companyBranchUpdateDto.BranchName;
                CompanyBranchObject.AddressId = companyBranchUpdateDto.AddressId;



                _customerAddressContext.CompanyBranches.Update(CompanyBranchObject);
                return await _customerAddressContext.SaveChangesAsync();
            }
        }

        public async Task<bool> AnyCompanyBranchUpdate(string CompanyBranchName, int id)
        {

            var CompanyBranchNames = _customerAddressContext.CompanyBranches.Where(p => !p.IsDeleted && p.Id != id).Select(p => p.BranchName).ToList();
            if (CompanyBranchNames.Contains(CompanyBranchName))
            {
                return await Task.FromResult(true);
            }
            else
            {
                return await Task.FromResult(false);
            }


        }

    }
}
