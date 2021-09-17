using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerAddress.Core.Entities;

namespace CustomerAddress.DAL.Entities
{
    public class Customer : Audit, ISoftDeleted, IEntity
    { 
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public string TelNumber { get; set; }
        public string Mail { get; set; }
        public bool CustomerType { get; set; }


        public int AddressId { get; set; }
        public Address AddressFK { get; set; }

        public ICollection<Post> SenderPosts { get; set; }
        public ICollection<Post> ReceiverPosts { get; set; }

        public bool IsDeleted { get; set ; }

    }
}
