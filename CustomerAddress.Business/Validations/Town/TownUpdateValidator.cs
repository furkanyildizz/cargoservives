using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerAddress.Business.Abstract;
using CustomerAddress.Business.Dtos.District;
using FluentValidation;

namespace CustomerAddress.Business.Validations.District
{
    public class TownUpdateValidator : AbstractValidator<TownUpdateDto>
    {
        private readonly ITownService _districtService;
        public TownUpdateValidator(ITownService districtService)
        {
            _districtService = districtService;

            RuleFor(p => p.CityId).NotEmpty().WithMessage("Şehir Id Boş Bırakılamaz").Must(AnyCity).WithMessage("Böyle bir il bulunmamaktadır.");
            RuleFor(p => p.TownName).NotEmpty().WithMessage("İlçe İsmi Boş Bırakılamaz").MaximumLength(100).WithMessage("İlçe max 100 karakter olmalı");

        }
        public bool AnyCity(int cityId)
        {
            return _districtService.AndCityId(cityId);
        }
    }
}
