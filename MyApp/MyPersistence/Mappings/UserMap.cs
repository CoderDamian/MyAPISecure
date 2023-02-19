using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyModels;

namespace MyPersistence.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("PERSON", "CIA");

            builder.Property(p => p.Name)
                .HasColumnName("PERSONID");

            builder.Property(p => p.Password)
                .HasColumnName("PASSWORD");
        }
    }
}
