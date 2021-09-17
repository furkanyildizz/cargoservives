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
    public class PostUpdateValidator : AbstractValidator<PostUpdateDto>
    {
        private IPostService _postService;
        public PostUpdateValidator(IPostService postService)
        {
            _postService = postService;



            RuleFor(p => p.PostStatus).NotEmpty().WithMessage("Posta statüsü Boş Bırakılamaz").Must(PostStatusChecker).WithMessage("1 Devam etmekte - 2 Teslim edildi olmalıdır.");
            RuleFor(p => p.SenderId).NotEmpty().WithMessage("Gönderici Boş Bırakılamaz").Must(AnySenderIds).WithMessage("Böyle bir gönderici bulunmamaktadır.");
            RuleFor(p => p.ReceiverId).NotEmpty().WithMessage("Alıcı Boş Bırakılamaz").Must(AnyReceivedIds).WithMessage("Böyle bir alıcı bulunmamaktadır.");
            RuleFor(p => p.CompanyBranchId).NotEmpty().WithMessage("Şube Boş Bırakılamaz").Must(AntCompanyBranchIds).WithMessage("Böyle bir şube bulunmamaktadır.");
            RuleFor(p => p.EmployeeId).NotEmpty().WithMessage("Çalışan Boş Bırakılamaz").Must(AnyEmployeeIds).WithMessage("Böyle bir çalışan bulunmamaktadır.");


        }



        public bool PostStatusChecker(byte postStatus)
        {
            if (postStatus == 1 || postStatus == 2)
                return true;
            else
                return false;
        }
        public bool AnySenderIds(int senderId)
        {
            var result = _postService.AnySenderIds(senderId);
            return result;
        }

        public bool AnyReceivedIds(int receivedId)
        {
            var result = _postService.AnyReceivedIds(receivedId);
            return result;
        }

        public bool AntCompanyBranchIds(int companyBranchId)
        {
            var result = _postService.AnyCompanyBranchIds(companyBranchId);
            return result;
        }

        public bool AnyEmployeeIds(int employeeId)
        {
            var result = _postService.AnyEmployeeIds(employeeId);
            return result;
        }
    }
}
