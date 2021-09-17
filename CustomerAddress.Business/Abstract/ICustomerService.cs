using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerAddress.Business.Dtos.Customer;

namespace CustomerAddress.Business.Abstract
{
    public interface ICustomerService
    {
        public Task<List<CustomerListDto>> GetCustomers();
        public Task<CustomerListDto> GetCustomersById(int id);
        //public Task<List<CustomerListDto>> GetCustomersByCityId(int id);

        //public Task<List<int>> GetCustomersByCityId(int id);

        //public Task<List<int>> GetCustomersByDistrictId(int id);
        //public Task<List<int>> GetCustomersByNeighborhoodId(int id);
        //public Task<List<int>> GetCustomersByStreetId(int id);
        public Task<List<CustomerListDto>> GetCustomersByAddressId(int id);

        Task<List<List<CustomerListDto>>> GetCustomersByCompanyBranchId(int companyBranchId);

        public bool AnyAddressId(int addressId);


        /// </summary>
        public Task AddCustomer(CustomerAddDto CustomerAddDto);

        /// <summary>
        /// ogrenci guncelleme servisi
        /// </summary>
        public Task<int> UpdateCustomer(int id, CustomerUpdateDto customerUpdateDto);

        /// <summary>
        /// ogrenci silme servisi
        /// </summary>
        public Task<int> DeleteCustomer(int id);
    }
}
