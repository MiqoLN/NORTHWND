using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NORTHWND.Core.Entities;

namespace NORTHWND.DAL.Configurations
{
    internal class ShipperConfiguration : IEntityTypeConfiguration<Shipper>
    {
        public void Configure(EntityTypeBuilder<Shipper> entity)
        {
            entity.Property(e => e.ShipperId).HasColumnName("ShipperID");

            entity.Property(e => e.CompanyName)
                .IsRequired()
                .HasMaxLength(40);

            entity.Property(e => e.Phone).HasMaxLength(24);
        }

    }
}
