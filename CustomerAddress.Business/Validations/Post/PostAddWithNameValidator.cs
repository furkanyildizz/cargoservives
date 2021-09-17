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
    public class PostAddWithNameValidator : AbstractValidator<PostAddNameDto>
    {
        private IPostService _postService;
        public PostAddWithNameValidator(IPostService postService)
        {
            _postService = postService;


            RuleFor(p => p.PostStatus).NotEmpty().WithMessage("Posta statüsü Boş Bırakılamaz");

            ////////////Gönderici
            RuleFor(p => p.SenderName).NotEmpty().WithMessage("Gönderici aDI Boş Bırakılamaz");
            RuleFor(p => p.SenderSurname).NotEmpty().WithMessage("Gönderici soyadı Boş Bırakılamaz");
            RuleFor(p => p.SenderMail).NotEmpty().WithMessage("Gönderici maili Boş Bırakılamaz");
            RuleFor(p => p.SenderTelNumber).NotEmpty().WithMessage("Gönderici Telefon Numarası Boş Bırakılamaz").Length(11,11).WithMessage("Telefon 11 haneli olmalıdır");
            
            RuleFor(p => p.SenderCity).NotEmpty().WithMessage("Gönderici Şehri Numarası Boş Bırakılamaz");
            RuleFor(p => p.SenderRegion).NotEmpty().WithMessage("Gönderici Bölgesi Numarası Boş Bırakılamaz");
            RuleFor(p => p.SenderTown).NotEmpty().WithMessage("Gönderici İlçesi Numarası Boş Bırakılamaz");
            RuleFor(p => p.SenderNeighborhood).NotEmpty().WithMessage("Gönderici Mahallesi Numarası Boş Bırakılamaz");
            RuleFor(p => p.SenderStreet).NotEmpty().WithMessage("Gönderici Sokağı Numarası Boş Bırakılamaz");
            RuleFor(p => p.SenderDescription).NotEmpty().WithMessage("Gönderici Açık Adresi Boş Bırakılamaz");
            
            ////Alıcıııı
            RuleFor(p => p.ReceiverName).NotEmpty().WithMessage("Alıcı aDI Boş Bırakılamaz");
            RuleFor(p => p.ReceiverSurname).NotEmpty().WithMessage("Alıcı soyadı Boş Bırakılamaz");
            RuleFor(p => p.ReceiverMail).NotEmpty().WithMessage("Alıcı maili Boş Bırakılamaz");
            RuleFor(p => p.ReceiverTelNumber).NotEmpty().WithMessage("Alıcı Telefon Numarası Boş Bırakılamaz").Length(11, 11).WithMessage("Telefon 11 haneli olmalıdır");
                           
            RuleFor(p => p.ReceiverCity).NotEmpty().WithMessage("Alıcı Şehri Numarası Boş Bırakılamaz");
            RuleFor(p => p.ReceiverRegion).NotEmpty().WithMessage("Alıcı Bölgesi Numarası Boş Bırakılamaz");
            RuleFor(p => p.ReceiverTown).NotEmpty().WithMessage("Alıcı İlçesi Numarası Boş Bırakılamaz");
            RuleFor(p => p.ReceiverNeighborhood).NotEmpty().WithMessage("Alıcı Mahallesi Numarası Boş Bırakılamaz");
            RuleFor(p => p.ReceiverStreet).NotEmpty().WithMessage("Alıcı Sokağı Numarası Boş Bırakılamaz");
            RuleFor(p => p.ReceiverDescription).NotEmpty().WithMessage("Alıcı Açık Adresi Boş Bırakılamaz");

            RuleFor(p => p.CompanyBranchId).NotEmpty().WithMessage("Şube boş bırakılamaz").Must(AnyCompanyBranchIds).WithMessage("Böyle bir şube bulunmamaktadır");
            RuleFor(p => p.EmployeeId).NotEmpty().WithMessage("Çalışan boş bırakılamaz").Must(AnyEmployeeIds).WithMessage("Böyle bir Çalışan bulunmamaktadır");
        }

        public bool AnyCompanyBranchIds(int companyBranchId)
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
