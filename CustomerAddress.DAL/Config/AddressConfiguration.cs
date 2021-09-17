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
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Title).HasMaxLength(100);
            builder.Property(p => p.Description).HasMaxLength(255);

            //builder.HasOne(p => p.CustomerFK).WithMany(p => p.Addresses).HasForeignKey(p => p.CustomerId);

            //Adres entityleri
            builder.HasOne(p => p.CityFK).WithMany(p => p.Addresses).HasForeignKey(p => p.CityId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.TownFK).WithMany(p => p.Addresses).HasForeignKey(p => p.TownId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.NeighborhoodFK).WithMany(p => p.Addresses).HasForeignKey(p => p.NeighborhoodId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.StreetFK).WithMany(p => p.Addresses).HasForeignKey(p => p.StreetId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
