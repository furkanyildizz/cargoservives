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
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.EmployeeName).HasMaxLength(150);
            builder.Property(p => p.EmployeeSurname).HasMaxLength(150);
            builder.Property(p => p.CompanyBranchId).IsRequired();


            builder.HasOne(p => p.CompanyBranchFK).WithMany(p => p.Employees).HasForeignKey(p => p.CompanyBranchId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
