using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerAddress.Business.Dtos.Status;

namespace CustomerAddress.Business.Abstract
{
    public interface IStatusService
    {
        public Task<List<StatusListDto>> GetStatuses();
        public Task<int> AddStatus(StatusAddDto statusAddDto);
        public Task<int> UpdateStatus(int statusId,StatusUpdateDto statusUpdateDto );
        public Task<int> DeleteStatus(int statusId);
    }
}
