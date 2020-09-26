namespace Analystor.Nishomi.Persistence
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public abstract class ActiveEntityConfiguration<T> : IEntityTypeConfiguration<T>
        where T : class
    {
        /// <summary>
        /// Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasQueryFilter(it => EF.Property<bool>(it, "IsActive") == true);
            //builder.Property<bool>("IsActive").HasDefaultValue(true);
        }
    }
}
