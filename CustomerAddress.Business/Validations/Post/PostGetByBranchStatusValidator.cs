using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerAddress.Business.Abstract;
using CustomerAddress.Business.Dtos.Post;
using FluentValidation;

namespace CustomerAddress.Business.Validations.Post
{
    public class PostGetByBranchStatusValidator : AbstractValidator<PostGetBranchStatusDto>
    {
        private IPostService _postService;
        public PostGetByBranchStatusValidator(IPostService postService)
        {
            _postService = postService;

            RuleFor(p => p.BranchId).NotEmpty().WithMessage("Branch Id Boş olamaz").Must(AnyBranchId).WithMessage("Böyle bir şube yok");
            RuleFor(p => p.PostStatus).NotEmpty().WithMessage("Posta durumu boş olamaz").Must(AnyPostStatus).WithMessage("Böyle bir statü yoktur");

        }

        public bool AnyBranchId(int branchId)
        {
            return _postService.AnyCompanyBranchIds(branchId);
        }
        public bool AnyPostStatus(byte postStatus)
        {
            if (postStatus == 1 || postStatus ==2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



    }
}
