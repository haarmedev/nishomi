namespace Analystor.Nishomi.Persistence
{
    using Analystor.Nishomi.Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ProductImagesConfiguration : ActiveEntityConfiguration<ProductImages>
    {
        public override void Configure(EntityTypeBuilder<ProductImages> builder)
        {
            builder.HasOne(it => it.Product)
                   .WithMany(it => it.ProductImages)
                   .OnDelete(DeleteBehavior.Cascade);

            base.Configure(builder);
        }
    }
}
