using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerAddress.Business.Abstract;
using CustomerAddress.Business.Dtos.CompanyBranch;
using FluentValidation;

namespace CustomerAddress.Business.Validations.CompanyBranch
{
    public class CompanyBranchAddValidator : AbstractValidator<CompanyBranchAddDto>
    {
        private ICompanyBranchService _companyBranchService;
        public CompanyBranchAddValidator(ICompanyBranchService companyBranchService)
        {
            _companyBranchService = companyBranchService;

            RuleFor(p => p.BranchName).NotEmpty().WithMessage("Şube ismi Boş Bırakılamaz").MaximumLength(150).WithMessage("Şubeismi 150 karakteri geçemez");
            RuleFor(p => p.AddressId).NotEmpty().WithMessage("AdresId Boş Bırakılamaz").Must(AnyAddress).WithMessage("Böyle bir adres bulunmamakta");

        }

        public bool AnyAddress(int addressId)
        {
            return _companyBranchService.AnyAddressId(addressId);
        }
    }
}
