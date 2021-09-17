using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerAddress.Business.Abstract;
using CustomerAddress.Business.Dtos.Status;
using FluentValidation;

namespace CustomerAddress.Business.Validations.Status
{
    public class StatusUpdateValidator : AbstractValidator<StatusUpdateDto>
    {
        private readonly IStatusService _statusService;
        public StatusUpdateValidator(IStatusService statusService)
        {
            _statusService = statusService;
            RuleFor(p => p.Description).NotEmpty().WithMessage("Açıklama boş olamaz");
        }
    }
}
