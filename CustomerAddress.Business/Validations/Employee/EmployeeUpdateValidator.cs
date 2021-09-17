using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerAddress.Business.Abstract;
using CustomerAddress.Business.Dtos.Employee;
using FluentValidation;

namespace CustomerAddress.Business.Validations.Employee
{
    public class EmployeeUpdateValidator : AbstractValidator<EmployeeUpdateDto>
    {
        private IEmployeeService _employeeService;
        public EmployeeUpdateValidator(IEmployeeService employeeService)
        {
            _employeeService = employeeService;


            RuleFor(p => p.CompanyBranchId).NotEmpty().WithMessage("Şube Id ismi Boş Bırakılamaz").Must(AnyCompanyBranchId).WithMessage("Böyle bir şube bulunmamaktadır");
            RuleFor(p => p.EmployeeName).NotEmpty().WithMessage("Çalışan ismi Boş Bırakılamaz").MaximumLength(200).WithMessage("Max 200 karakter olmalı");
            RuleFor(p => p.EmployeeSurname).NotEmpty().WithMessage("Çalışan soyismi Boş Bırakılamaz").MaximumLength(200).WithMessage("Max 200 karakter olmalı");



        }

        public bool AnyCompanyBranchId(int companyBranchId)
        {
            return _employeeService.AnyCompanyBranchId(companyBranchId);
        }
    }
}
