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
    public class NeighborhoodConfiguration : IEntityTypeConfiguration<Neighborhood>
    {
        public void Configure(EntityTypeBuilder<Neighborhood> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.NeighborhoodName).HasMaxLength(150);

            builder.HasOne(p => p.TownFK).WithMany(p => p.Neighborhoods).HasForeignKey(p => p.TownId).OnDelete(DeleteBehavior.Restrict);
            //builder.HasMany(p => p.Addresses).WithOne(p => p.NeighborhoodFK).HasForeignKey(p => p.NeighborhoodId).OnDelete(DeleteBehavior.Cascade);
            //builder.HasMany(p => p.Streets).WithOne(p => p.NeighborhoodFK).HasForeignKey(p => p.NeighborhoodId);
        }
    }
}
