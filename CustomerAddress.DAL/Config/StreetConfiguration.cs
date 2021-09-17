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
    public class StreetConfiguration : IEntityTypeConfiguration<Street>
    {
        public void Configure(EntityTypeBuilder<Street> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.StreetName).HasMaxLength(150);

            builder.HasOne(p => p.NeighborhoodFK).WithMany(p => p.Streets).HasForeignKey(p => p.NeighborhoodId).OnDelete(DeleteBehavior.Restrict);



            //builder.HasMany(p => p.Addresses).WithOne(p => p.StreetFK).HasForeignKey(p => p.StreetId);
        }
    }
}
