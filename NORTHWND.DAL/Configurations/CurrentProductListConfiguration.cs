using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NORTHWND.Core.Entities;

namespace NORTHWND.DAL.Configurations
{
    internal class CurrentProductListConfiguration : IEntityTypeConfiguration<CurrentProductList>
    {
        public void Configure(EntityTypeBuilder<CurrentProductList> entity)
        {

            entity.HasNoKey();

            entity.ToView("Current Product List");

            entity.Property(e => e.ProductId)
                .ValueGeneratedOnAdd()
                .HasColumnName("ProductID");

            entity.Property(e => e.ProductName)
                .IsRequired()
                .HasMaxLength(40);

        }

    }
}
