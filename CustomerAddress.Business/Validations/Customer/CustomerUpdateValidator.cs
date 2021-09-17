using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerAddress.Business.Abstract;
using CustomerAddress.Business.Dtos.Customer;
using FluentValidation;

namespace CustomerAddress.Business.Validations.Customer
{
    public class CustomerUpdateValidator : AbstractValidator<CustomerUpdateDto>
    {
        private ICustomerService _customerService;
        public CustomerUpdateValidator(ICustomerService customerService)
        {
            _customerService = customerService;

            RuleFor(p => p.CustomerName).NotEmpty().WithMessage("İsim Boş Bırakılamaz").MaximumLength(100).WithMessage("İsim 100 karakteri geçemez");
            RuleFor(p => p.CustomerSurname).NotEmpty().WithMessage("Soyisim Boş Bırakılamaz").MaximumLength(100).WithMessage("Soyisim 100 karakteri geçemez");
            RuleFor(p => p.CustomerType).NotEmpty().WithMessage("0 Alıcı- 1 Gönderici belirtiniz");
            RuleFor(p => p.Mail).NotEmpty().WithMessage("Mail Boş Bırakılamaz").MaximumLength(150).WithMessage("Mail 150 karakteri geçemez").EmailAddress().WithMessage("Geçerli Mail adresi giriniz");
            RuleFor(p => p.TelNumber).NotEmpty().WithMessage("Telefon Boş Bırakılamaz").Length(11, 11).WithMessage("Telefon 11 karakter olmalı");
            RuleFor(p => p.AddressId).NotEmpty().WithMessage("Adres Boş Bırakılamaz").Must(AnyAddressId).WithMessage("Böyle bir address bulunmamaktadır.");
        }

        public bool AnyAddressId(int addressId)
        {
            return _customerService.AnyAddressId(addressId);
        }
    }
}
