using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerAddress.Business.Abstract;
using CustomerAddress.Business.Dtos.City;
using FluentValidation;

namespace CustomerAddress.Business.Validations.City
{
    public class CityUpdateValidator : AbstractValidator<CityUpdateDto>
    {
        private ICityService _cityService;
        public CityUpdateValidator(ICityService cityService)
        {
            _cityService = cityService;


            RuleFor(p => p.CityName).NotEmpty().WithMessage("Şehir ismi Boş Bırakılamaz").MaximumLength(100).WithMessage("şehir 100 karakteri geçemez");
            RuleFor(p => p.CityRegion).NotEmpty().WithMessage("Bölge Boş Bırakılamaz").MaximumLength(100).WithMessage("Bölge 100 karakteri geçemez");


        }
    }
}
