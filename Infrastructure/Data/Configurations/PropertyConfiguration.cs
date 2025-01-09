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
    public class PropertyConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.HasKey(p => p.IdProperty);
            builder.Property(p => p.IdProperty).UseIdentityColumn();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(255);
            builder.Property(p => p.Address).IsRequired().HasMaxLength(255);
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
            builder.Property(p => p.CodeInternal).IsRequired().HasMaxLength(20);
            builder.Property(p => p.Year).IsRequired();
            builder.HasOne(p => p.Owner).WithMany(o => o.Properties).HasForeignKey(p => p.IdOwner);
            builder.HasIndex(p => p.CodeInternal).IsUnique();
            builder.ToTable("Properties");
        }
    }
}
