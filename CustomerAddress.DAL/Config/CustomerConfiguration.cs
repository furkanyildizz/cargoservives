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
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.CustomerName).HasMaxLength(155);
            builder.Property(p => p.CustomerSurname).HasMaxLength(155);
            builder.Property(p => p.TelNumber).HasMaxLength(11);
            builder.Property(p => p.Mail).HasMaxLength(100);


            builder.HasOne(p => p.AddressFK).WithMany(p => p.Customers).HasForeignKey(p => p.AddressId).OnDelete(DeleteBehavior.Restrict);

            //builder.HasMany(p => p.Addresses).WithOne(p => p.CustomerFK).HasForeignKey(p => p.CustomerId);

        }
    }
}
