using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerAddress.Business.Dtos.CompanyBranch;

namespace CustomerAddress.Business.Abstract
{
    public interface ICompanyBranchService
    {
        public Task<List<CompanyBranchListDto>> GetCompanyBranches();
        public Task<CompanyBranchListDto> GetCompanyBranchesById(int id);

        public Task<bool> AnyCompanyBranch(string CompanyBranchName);

        /// </summary>
        /// 
        public bool AnyAddressId(int addressId);
        public Task<int> AddCompanyBranch(CompanyBranchAddDto CompanyBranchAddDto);

        /// <summary>
        /// ogrenci guncelleme servisi
        /// </summary>
        public Task<int> UpdateCompanyBranch(int id, CompanyBranchUpdateDto companyBranchUpdateDto);

        /// <summary>
        /// ogrenci silme servisi
        /// </summary>
        public Task<int> DeleteCompanyBranch(int id);
    }
}
