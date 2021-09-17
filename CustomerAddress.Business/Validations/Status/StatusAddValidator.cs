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
    public class StatusAddValidator:AbstractValidator<StatusAddDto>
    {
        private readonly IStatusService _statusService;
        public StatusAddValidator(IStatusService statusService)
        {
            _statusService = statusService;

            RuleFor(p => p.Description).NotEmpty().WithMessage("Açıklama Boş olamaz");

        }
    }
}
