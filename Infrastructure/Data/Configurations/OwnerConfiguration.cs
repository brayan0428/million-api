using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Configurations
{
    public class OwnerConfiguration : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> builder)
        {
            builder.HasKey(p => p.IdOwner);
            builder.Property(p => p.IdOwner).UseIdentityColumn();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(255);
            builder.Property(p => p.Address).IsRequired().HasMaxLength(255);
            builder.Property(p => p.Photo).HasColumnType("varbinary(max)");
            builder.Property(p => p.Birthday).HasColumnType("date");
            builder.HasMany(o => o.Properties).WithOne(p => p.Owner).HasForeignKey(p => p.IdOwner);
            builder.ToTable("Owners");
        }
    }
}
