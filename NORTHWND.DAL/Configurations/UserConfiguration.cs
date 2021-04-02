using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NORTHWND.Core.Entities;

namespace NORTHWND.DAL.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Email).HasColumnType("VARCHAR(100)");
            entity.Property(x => x.Password).HasColumnType("VARCHAR(100)");
        }

    }
}
