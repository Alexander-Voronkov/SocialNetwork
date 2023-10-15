using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder) 
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .ValueGeneratedNever();

            builder
                .Property(x => x.Username)
                .IsRequired();

            builder
                .Property(x => x.EmailConfirmed)
                .IsRequired();

            builder
               .Property(x => x.PhoneConfirmed)
               .IsRequired();

            builder
                .Property(x => x.Email)
                .IsRequired();

            builder
                .Property(x => x.PhoneNumber)
                .IsRequired();

            builder
                .HasQueryFilter(x => !x.IsDeleted);

        }
    }
}
