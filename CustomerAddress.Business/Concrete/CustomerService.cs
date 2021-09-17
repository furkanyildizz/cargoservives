using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerAddress.Business.Abstract;
using CustomerAddress.Business.Dtos.Customer;
using CustomerAddress.DAL.Context;
using CustomerAddress.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerAddress.Business.Concrete
{
    public class CustomerService : ICustomerService
    {
        private readonly CargoDbContext _customerAddressContext;
        public CustomerService(CargoDbContext customerAddressContext)
        {
            _customerAddressContext = customerAddressContext;
        }
        public async Task AddCustomer(CustomerAddDto CustomerAddDto)
        {

            var newCustomer = new Customer
            {
                AddressId = CustomerAddDto.AddressId,
                CustomerType = CustomerAddDto.CustomerType,
                CustomerName = CustomerAddDto.CustomerName,
                CustomerSurname = CustomerAddDto.CustomerSurname,
                Mail = CustomerAddDto.Mail,
                TelNumber = CustomerAddDto.TelNumber

            };
            _customerAddressContext.Customers.Add(newCustomer);
            await _customerAddressContext.SaveChangesAsync();
        }

        public bool AnyAddressId(int addressId)
        {
            return _customerAddressContext.Addresses.Where(p => !p.IsDeleted).Any(p => p.Id == addressId);
        }

        public async Task<int> DeleteCustomer(int id)
        {
            var result = await _customerAddressContext.Posts.Where(p => !p.IsDeleted).AnyAsync(p => p.EmployeeId == id || p.ReceiverId == id);
            var CustomerObject = _customerAddressContext.Customers.SingleOrDefault(p => !p.IsDeleted && p.Id == id);

            if (result)
            {
                return await Task.FromResult(-2);
            }
            else
            {
                if (CustomerObject != null)
                {
                    CustomerObject.IsDeleted = true;
                    _customerAddressContext.Customers.Update(CustomerObject);
                    return await _customerAddressContext.SaveChangesAsync();
                }
                else
                {
                    return await Task.FromResult(-1);
                }
            }


        }

        public Task<List<CustomerListDto>> GetCustomers()
        {
            return _customerAddressContext.Customers.Where(p => !p.IsDeleted).Select(p => new CustomerListDto
            {
                Id = p.Id,
                AddressId = p.AddressId,
                CustomerType = p.CustomerType,
                CustomerName = p.CustomerName,
                CustomerSurname = p.CustomerSurname,
                Mail = p.Mail,
                TelNumber = p.TelNumber

            }).ToListAsync();
        }

        public async Task<List<CustomerListDto>> GetCustomersByAddressId(int id)
        {

            return await _customerAddressContext.Customers.Where(p => !p.IsDeleted && p.AddressId == id).Select(p => new CustomerListDto
            {
                Id = p.Id,
                AddressId = p.AddressId,
                CustomerType = p.CustomerType,
                CustomerName = p.CustomerName,
                CustomerSurname = p.CustomerSurname,
                Mail = p.Mail,
                TelNumber = p.TelNumber

            }).ToListAsync();

        }

        public async Task<List<List<CustomerListDto>>> GetCustomersByCompanyBranchId(int companyBranchId)
        {
            var posts = await _customerAddressContext.Posts.Where(p => !p.IsDeleted && p.CompanyBranchId == companyBranchId).ToListAsync();
            var customerList = new List<List<CustomerListDto>>();


            foreach (var post in posts)
            {
                var customer = await _customerAddressContext.Customers.Where(p => !p.IsDeleted && (p.Id == post.SenderId || p.Id == post.ReceiverId))
                                                                        .Select(p => new CustomerListDto
                                                                        {
                                                                            Id = p.Id,
                                                                            AddressId = p.AddressId,
                                                                            CustomerType = p.CustomerType,
                                                                            CustomerName = p.CustomerName,
                                                                            CustomerSurname = p.CustomerSurname,
                                                                            Mail = p.Mail,
                                                                            TelNumber = p.TelNumber
                                                                        }).ToListAsync();

                customerList.Add(customer);
            }

            return customerList;
        }


        //public async Task<List<int>> GetCustomersByDistrictId(int id)
        //{
        //    List<int> customersId = new List<int>();
        //    customersId = await _customerAddressContext.Addresses.Where(p => !p.IsDeleted && p.StreetFK.NeighborhoodFK.DistrictId == id).Select(p => p.CustomerId).ToListAsync();
        //    return customersId;


        //}


        //public async Task<List<int>> GetCustomersByStreetId(int id)
        //{
        //    List<int> customersId = new List<int>();
        //    customersId = await _customerAddressContext.Addresses.Where(p => !p.IsDeleted && p.StreetId == id).Select(p => p.CustomerId).ToListAsync();
        //    return customersId;

        //}


        //public async Task<List<int>> GetCustomersByNeighborhoodId(int id)
        //{
        //    List<int> customersId = new List<int>();
        //    customersId = await _customerAddressContext.Addresses.Where(p => !p.IsDeleted && p.StreetFK.NeighborhoodId == id).Select(p => p.CustomerId).ToListAsync();
        //    return customersId;


        //}

        public bool AnyCustomerId(int id)
        {

            return _customerAddressContext.Customers.Where(p => !p.IsDeleted).Any(p => p.Id == id);
        }

        public Task<CustomerListDto> GetCustomersById(int id)
        {
            return _customerAddressContext.Customers.Where(p => !p.IsDeleted)
           .Select(p => new CustomerListDto
           {
               Id = p.Id,
               AddressId = p.AddressId,
               CustomerType = p.CustomerType,
               CustomerName = p.CustomerName,
               CustomerSurname = p.CustomerSurname,
               Mail = p.Mail,
               TelNumber = p.TelNumber

           }).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<int> UpdateCustomer(int id, CustomerUpdateDto customerUpdateDto)
        {
            var CustomerObject = await _customerAddressContext.Customers.SingleOrDefaultAsync(p => !p.IsDeleted && p.Id == id);

            if (CustomerObject == null)
            {
                return await Task.FromResult(-1);
            }
            CustomerObject.AddressId = customerUpdateDto.AddressId;
            CustomerObject.CustomerType = customerUpdateDto.CustomerType;
            CustomerObject.CustomerName = customerUpdateDto.CustomerName;
            CustomerObject.CustomerSurname = customerUpdateDto.CustomerSurname;
            CustomerObject.Mail = customerUpdateDto.Mail;
            CustomerObject.TelNumber = customerUpdateDto.TelNumber;




            _customerAddressContext.Customers.Update(CustomerObject);
            return await _customerAddressContext.SaveChangesAsync();
        }


    }
}
