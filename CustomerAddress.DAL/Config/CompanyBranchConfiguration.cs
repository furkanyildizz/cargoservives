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
    public class CompanyBranchConfiguration : IEntityTypeConfiguration<CompanyBranch>
    {
        public void Configure(EntityTypeBuilder<CompanyBranch> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.BranchName).HasMaxLength(200);
            builder.Property(p => p.AddressId).IsRequired();


            builder.HasOne(p => p.AddressFK).WithMany(p => p.CompanyBranches).HasForeignKey(p => p.AddressId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
