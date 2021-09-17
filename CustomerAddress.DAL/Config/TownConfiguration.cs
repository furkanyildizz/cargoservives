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
    public class TownConfiguration : IEntityTypeConfiguration<Town>
    {
        public void Configure(EntityTypeBuilder<Town> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.TownName).HasMaxLength(150);


            builder.HasOne(p => p.CityFK).WithMany(p => p.Towns).HasForeignKey(p => p.CityId).OnDelete(DeleteBehavior.Restrict);



            //builder.HasMany(p => p.Addresses).WithOne(p => p.DistrictFK).HasForeignKey(p => p.DistrictId).OnDelete(DeleteBehavior.Cascade);
            //builder.HasMany(p => p.Neighborhoods).WithOne(p => p.DistrictFK).HasForeignKey(p => p.DistrictId);

        }
    }
}
