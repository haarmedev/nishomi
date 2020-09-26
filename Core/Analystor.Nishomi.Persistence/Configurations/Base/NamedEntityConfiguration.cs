namespace Analystor.Nishomi.Persistence
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public abstract class NamedEntityConfiguration<T> : ActiveEntityConfiguration<T>
        where T : class
    {
        /// <summary>
        /// Configures the specified builder.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public override void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property("Name").IsRequired();

            base.Configure(builder);
        }
    }
}
