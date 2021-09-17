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
    public class RouteMapAddValidator : AbstractValidator<RouteMapAddDto>
    {
        private IRouteMapService _routeMapService;
        public RouteMapAddValidator(IRouteMapService routeMapService)
        {
            _routeMapService = routeMapService;
            RuleFor(p => p.CompanyBranchId).NotEmpty().WithMessage("Şube ismi boş olamaz");
            RuleFor(p => p.PostId).NotEmpty().WithMessage("Posta Id boş olamaz").Must(AnyPost).WithMessage("Böyle bir posta bulunmamaktadır");
            RuleFor(p => p.StatusId).NotEmpty().WithMessage("Posta statüsü boş olamaz");
            
            
        }

        public  bool AnyPost(int PostId)
        {
            return  _routeMapService.AnyPost(PostId);
        }
    }
}
