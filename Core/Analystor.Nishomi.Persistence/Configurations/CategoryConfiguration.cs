namespace Analystor.Nishomi.Persistence
{
    using Analystor.Nishomi.Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CategoryConfiguration : NamedEntityConfiguration<Category>
    {
        /// <summary>
        /// Configures the specified builder.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasMany(it => it.Products)
                   .WithOne(it => it.Category)
                   .OnDelete(DeleteBehavior.Cascade);

            base.Configure(builder);
        }
    }
}
