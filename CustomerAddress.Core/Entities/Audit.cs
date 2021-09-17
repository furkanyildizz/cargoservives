using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAddress.Core.Entities
{
    public class Audit
    {
        public DateTime? Cdate { get; set; }
        public DateTime? Mdate { get; set; }
        public int Cuser { get; set; }
        public int Muser { get; set; }
    }
}
