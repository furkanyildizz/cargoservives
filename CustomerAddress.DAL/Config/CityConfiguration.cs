using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerAddress.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerAddress.DAL.Config
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.CityName).HasMaxLength(200);
            builder.Property(p => p.CityRegion).HasMaxLength(200);

            //builder.HasMany(p => p.Addresses).WithOne(p => p.CityFK).HasForeignKey(p => p.CityId).OnDelete(DeleteBehavior.Cascade);
            //builder.HasMany(p => p.Districts).WithOne(p => p.CityFK).HasForeignKey(p => p.CityId);
            
        }
    }
}
