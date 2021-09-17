using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerAddress.Business.Abstract;
using CustomerAddress.Business.Dtos.RouteMap;
using FluentValidation;

namespace CustomerAddress.Business.Validations.RouteMap
{
    public class RouteMapConfirmValidator: AbstractValidator<RouteMapConfirmDto>
    {
        private IRouteMapService _routeMapService;
        public RouteMapConfirmValidator(IRouteMapService routeMapService)
        {
            _routeMapService = routeMapService;
            RuleFor(p => p.CompanyBranchId).NotEmpty().WithMessage("Şube boş bırakılamaz");
            RuleFor(p => p.StateId).NotEmpty().WithMessage("Durum boş bırakılamaz");

        }

    }
}
