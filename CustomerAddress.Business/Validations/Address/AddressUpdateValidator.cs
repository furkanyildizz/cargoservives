using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerAddress.Business.Abstract;
using CustomerAddress.Business.Dtos.Address;
using FluentValidation;

namespace CustomerAddress.Business.Validations.Address
{
    public class AddressUpdateValidator : AbstractValidator<AddressUpdateDto>
    {

        private IAddressService _addressService;
        private IStreetService _streetService;
        private INeighborhoodService _neighborhoodService;
        private ICityService _cityService;
        private ITownService _townService;
        public AddressUpdateValidator(IAddressService addressService, IStreetService streetService, INeighborhoodService neighborhoodService, ICityService cityService, ITownService townService)
        {
            _streetService = streetService;
            _addressService = addressService;
            _neighborhoodService = neighborhoodService;
            _cityService = cityService;
            _townService = townService;

            RuleFor(p => p.StreetId).NotEmpty().WithMessage("Sokak Id Boş Bırakılamaz").Must(AnyStreetIds).WithMessage("Böyle bir sokak id yok");
            RuleFor(p => p.NeighborhoodId).NotEmpty().WithMessage("Mahalle Id Boş Bırakılamaz").Must(AnyNeighborhoodIds).WithMessage("Böyle mahallle yok");
            RuleFor(p => p.CityId).NotEmpty().WithMessage("City Id Boş Bırakılamaz").Must(AntCityIds).WithMessage("Böyle il  yok");
            RuleFor(p => p.TownId).NotEmpty().WithMessage("İlçe Id Boş Bırakılamaz").Must(AnyTownIds).WithMessage("Böyle ilçe yok");
            RuleFor(p => p.Title).NotEmpty().WithMessage("Başlık Boş Bırakılamaz").MaximumLength(50).WithMessage("Başlık 50 karakteri geçemez");
            RuleFor(p => p.Description).NotEmpty().WithMessage("Açıklama Boş Bırakılamaz").MaximumLength(255).WithMessage("Açıklama 255 karakteri geçemez");


        }


        public bool AnyStreetIds(int streetId)
        {
            var result = _streetService.AnyStreetId(streetId);
            return result;
        }

        public bool AnyNeighborhoodIds(int neighborhoodId)
        {
            var result = _neighborhoodService.AnyNeighborhoodId(neighborhoodId);
            return result;
        }

        public bool AntCityIds(int cityId)
        {
            var result = _cityService.AnyCityIds(cityId);
            return result;
        }

        public bool AnyTownIds(int townId)
        {
            var result = _townService.AndTownId(townId);
            return result;
        }
    }
}
