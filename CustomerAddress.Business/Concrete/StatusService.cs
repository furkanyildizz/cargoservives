using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerAddress.Business.Abstract;
using CustomerAddress.Business.Dtos.Status;
using CustomerAddress.DAL.Context;
using CustomerAddress.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerAddress.Business.Concrete
{
    public class StatusService : IStatusService
    {
        private readonly CargoDbContext _cargoDbContext;

        public StatusService(CargoDbContext cargoDbContext)
        {
            _cargoDbContext = cargoDbContext;
        }

        public async Task<int> AddStatus(StatusAddDto statusAddDto)
        {
            
                var newStatus = new Status
                {
                    Description = statusAddDto.Description,

                };
                _cargoDbContext.Statuses.Add(newStatus);
                 return await _cargoDbContext.SaveChangesAsync();
                
        }

        public async Task<int> DeleteStatus(int statusId)
        {
            var result = await _cargoDbContext.Statuses.Where(p => !p.IsDeleted && p.Id == statusId).SingleOrDefaultAsync();
            if (result != null)
            {
                result.IsDeleted = true;
                _cargoDbContext.Update(result);
                return await _cargoDbContext.SaveChangesAsync();
            }
            else
            {
                return await Task.FromResult(-1);
            }
           
        }

        public async Task<List<StatusListDto>> GetStatuses()
        {
            return await _cargoDbContext.Statuses.Where(p => !p.IsDeleted).Select(p => new StatusListDto
            {
                Id = p.Id,
                Description = p.Description,
            }

            ).ToListAsync();
        }

        public async Task<int> UpdateStatus(int statusId, StatusUpdateDto statusUpdateDto)
        {
            var status = _cargoDbContext.Statuses.Where(p => !p.IsDeleted && p.Id == statusId).SingleOrDefault();
            if (status != null)
            {
                status.Description = statusUpdateDto.Description;
                _cargoDbContext.Update(status);
                return await _cargoDbContext.SaveChangesAsync();
            }
            else
            {
                return await Task.FromResult(-1);
            }
            
            
        }
    }
}
