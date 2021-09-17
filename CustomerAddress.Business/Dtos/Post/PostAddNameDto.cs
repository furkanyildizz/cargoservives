using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAddress.Business.Dtos.Post
{
    public class PostAddNameDto
    {
        public byte PostStatus { get; set; }

        public string SenderName { get; set; }
        public string SenderSurname { get; set; }
        public string SenderMail { get; set; }
        public string SenderTelNumber { get; set; }

        public string SenderRegion { get; set; }
        public string SenderCity { get; set; }
        public string SenderTown { get; set; }
        public string SenderNeighborhood { get; set; }
        public string SenderStreet { get; set; }
        public string SenderDescription { get; set; }

        public string ReceiverName { get; set; }
        public string ReceiverSurname { get; set; }
        public string ReceiverMail { get; set; }
        public string ReceiverTelNumber { get; set; }
        
        public string ReceiverCity { get; set; }
        public string ReceiverRegion { get; set; }
        public string ReceiverTown { get; set; }
        public string ReceiverNeighborhood { get; set; }
        public string ReceiverStreet { get; set; }
        public string ReceiverDescription { get; set; }

        public int CompanyBranchId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime? ShippingStartDate { get; set; }
        public DateTime? ShippingFinishDate { get; set; }


    }
}
