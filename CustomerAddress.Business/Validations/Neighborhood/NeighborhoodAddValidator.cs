using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerAddress.Business.Abstract;
using CustomerAddress.Business.Dtos.District;
using CustomerAddress.Business.Dtos.Neighborhood;
using FluentValidation;

namespace CustomerAddress.Business.Validations.Neighborhood
{
    public class NeighborhoodAddValidator : AbstractValidator<NeighborhoodAddDto>
    {
        private readonly INeighborhoodService _neighborhoodService;
        public NeighborhoodAddValidator(INeighborhoodService neighborhoodService)
        {
            _neighborhoodService = neighborhoodService;

            RuleFor(p => p.TownId).NotEmpty().WithMessage("İlçe Id Boş Bırakılamaz").Must(AnyTownId).WithMessage("Böyle bir ilçe bulunmamaktadır.");
            RuleFor(p => p.NeighborhoodName).NotEmpty().WithMessage("Mahalle İsmi Boş Bırakılamaz").MaximumLength(100).WithMessage("Mahalle max 100 karakter olmalı");

        }

        public bool AnyTownId(int townId)
        {
            return _neighborhoodService.AnyTownId(townId);
        }

    }
}
