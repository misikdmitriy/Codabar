using CodabarWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodabarWeb.Configurations
{
    public class UnitEntityTypeConfiguration : IEntityTypeConfiguration<Unit>
    {
        public void Configure(EntityTypeBuilder<Unit> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.IsFloat).IsRequired();
            builder.Property(x => x.Name).IsRequired();
        }
    }
}
