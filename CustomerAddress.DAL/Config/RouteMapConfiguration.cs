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
    public class RouteMapConfiguration : IEntityTypeConfiguration<RouteMap>
    {
        public void Configure(EntityTypeBuilder<RouteMap> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();




            builder.HasOne(p => p.CompanyBranchFK).WithMany(p => p.RouteMaps).HasForeignKey(p => p.CompanyBranchId);
            builder.HasOne(p => p.StatusFK).WithMany(p => p.RouteMaps).HasForeignKey(p => p.StatusId);
            builder.HasOne(p => p.PostFK).WithMany(p => p.RouteMaps).HasForeignKey(p => p.PostId);
        }
    }
}
