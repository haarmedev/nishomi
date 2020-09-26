namespace Analystor.Nishomi.Persistence
{
    using Analystor.Nishomi.Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ProductConfiguration : NamedEntityConfiguration<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasMany(it => it.ProductImages)
                   .WithOne(it => it.Product)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(it => it.Category)
                   .WithMany(it => it.Products)
                   .OnDelete(DeleteBehavior.Cascade);

            base.Configure(builder);
        }
    }
}
