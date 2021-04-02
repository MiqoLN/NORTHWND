using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NORTHWND.Core.Entities;

namespace NORTHWND.DAL.Configurations
{
    internal class GetRandViewConfiguration : IEntityTypeConfiguration<GetRandView>
    {
        public void Configure(EntityTypeBuilder<GetRandView> entity)
        {
            entity.HasNoKey();

            entity.ToView("GetRandView");

            entity.Property(e => e.Value).HasColumnName("VALUE");
        }

    }
}
