using CodabarWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodabarWeb.Configurations
{
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Code).IsRequired();
            builder.Property(x => x.Remained).IsRequired();
            builder.HasOne(x => x.Unit)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
