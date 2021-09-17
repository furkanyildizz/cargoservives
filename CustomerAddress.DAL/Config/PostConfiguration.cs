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
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.PostCode).HasMaxLength(13);
            builder.Property(p => p.PostCode).IsRequired();
            builder.Property(p => p.PostStatus).IsRequired();
            builder.Property(p => p.CompanyBranchId).IsRequired();
            builder.Property(p => p.EmployeeId).IsRequired();
            builder.Property(p => p.SenderId).IsRequired();
            builder.Property(p => p.ReceiverId).IsRequired();



            builder.HasOne(p => p.CompanyBranchFK).WithMany(p => p.Posts).HasForeignKey(p => p.CompanyBranchId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.EmployeeFK).WithMany(p => p.Posts).HasForeignKey(p => p.EmployeeId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.SenderFK).WithMany(p => p.SenderPosts).HasForeignKey(p => p.SenderId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.ReceiverFK).WithMany(p => p.ReceiverPosts).HasForeignKey(p => p.ReceiverId).OnDelete(DeleteBehavior.Restrict);


        }
    }
}
