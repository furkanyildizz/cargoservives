using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerAddress.Business.Abstract;
using CustomerAddress.Business.Dtos.Street;
using FluentValidation;

namespace CustomerAddress.Business.Validations.Street
{
    public class StreetUpdateValidator : AbstractValidator<StreetUpdateDto>
    {
        private readonly IStreetService _streetService;
        public StreetUpdateValidator(IStreetService streetService)
        {
            _streetService = streetService;
            RuleFor(p => p.NeighborhoodId).NotEmpty().WithMessage("Mahalle Id Boş Bırakılamaz").Must(AnyNeighborhood).WithMessage("Böyle bir mahalle bulunmamaktadır");
            RuleFor(p => p.StreetName).NotEmpty().WithMessage("Sokak İsmi Boş Bırakılamaz").MaximumLength(100).WithMessage("Sokak max 100 karakter olmalı");
        }

        public bool AnyNeighborhood(int neighborhoodId)
        {
            return _streetService.AnyNeighborhood(neighborhoodId);
        }

    }
}
