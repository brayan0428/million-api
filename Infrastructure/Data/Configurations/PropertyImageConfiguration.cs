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
    public class PropertyImageConfiguration : IEntityTypeConfiguration<PropertyImage>
    {
        public void Configure(EntityTypeBuilder<PropertyImage> builder)
        {
            builder.HasKey(p => p.IdPropertyImage);
            builder.Property(p => p.IdPropertyImage).UseIdentityColumn();
            builder.Property(p => p.Url).IsRequired();
            builder.Property(p => p.Enabled).IsRequired();
            builder.HasOne(p => p.Property).WithMany(p => p.PropertyImages).HasForeignKey(p => p.IdProperty);
        }
    }
}
