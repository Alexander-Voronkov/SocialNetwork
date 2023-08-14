using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class ReactionConfiguration : IEntityTypeConfiguration<Reaction>
    {
        public void Configure(EntityTypeBuilder<Reaction> builder)
        {
        }
    }
}
