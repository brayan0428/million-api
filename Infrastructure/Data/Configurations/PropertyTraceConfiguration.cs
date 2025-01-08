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
    public class PropertyTraceConfiguration : IEntityTypeConfiguration<PropertyTrace>
    {
        public void Configure(EntityTypeBuilder<PropertyTrace> builder)
        {
            builder.HasKey(p => p.IdPropertyTrace);
            builder.Property(p => p.IdPropertyTrace).UseIdentityColumn();
            builder.Property(p => p.DateSale).IsRequired().HasColumnType("datetime");
            builder.Property(p => p.Name).IsRequired().HasMaxLength(500);
            builder.Property(p => p.Value).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(p => p.Tax).IsRequired().HasColumnType("decimal(18,2)");
            builder.HasOne(p => p.Property).WithMany(p => p.PropertyTraces).HasForeignKey(p => p.IdProperty);
        }
    }
}
