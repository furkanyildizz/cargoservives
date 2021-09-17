using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAddress.Business.Dtos.Post
{
    public class PostListNameDto
    {
        public int Id { get; set; }
        public string PostCode { get; set; }
        //public byte PostStatus { get; set; }
        public string PostStatus { get; set; }
        public string SenderName { get; set; }
        public string SenderSurname { get; set; }
        public string SenderAddresss { get; set; }
        public string SenderMail { get; set; }
        public string SenderTelNumber { get; set; }

        public string ReceiverName { get; set; }
        public string ReceiverSurname { get; set; }
        public string ReceiverAddresss { get; set; }
        public string ReceiverMail { get; set; }
        public string ReceiverTelNumber { get; set; }

        public string CompanyBranchName { get; set; }
        public string CompanyBranchNAddress{ get; set; }

        public string EmployeeName { get; set; }
        public string EmployeeSurname { get; set; }

        public DateTime? ShippingStartDate { get; set; }
        public DateTime? ShippingFinishDate { get; set; }
    }
}
