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
    public class RouteMapByPostUpdateValidator: AbstractValidator<RouteMapCodeUpdateDto>
    {
        private IRouteMapService _routeMapService;

        public RouteMapByPostUpdateValidator(IRouteMapService routeMapService)
        {
            _routeMapService = routeMapService;
            RuleFor(p => p.CompanyBranchId).NotEmpty().WithMessage("Şube ismi boş olamaz");
            RuleFor(p => p.StatusId).NotEmpty().WithMessage("Posta statüsü boş olamaz");


        }

        public bool AnyPost(int PostId)
        {
            return _routeMapService.AnyPost(PostId);
        }
    }
}
