using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyModels;

namespace MyPersistence.Mappings
{
    public class UserRefreshTokeMap : IEntityTypeConfiguration<UserRefreshToken>
    {
        public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
        {
            builder.ToTable("userrefreshtokens", "cia");

            builder.Property(p => p.Id)
                .HasColumnName("ID");

            builder.Property(p => p.UserName)
                .HasColumnName("USERNAME");

            builder.Property(p => p.RefreshToken)
                .HasColumnName("REFRESHTOKEN");

            builder.Property(p => p.IsActive)
                .HasColumnName("ISACTIVE");
        }
    }
}
