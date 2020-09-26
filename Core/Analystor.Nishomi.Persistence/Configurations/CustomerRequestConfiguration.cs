namespace Analystor.Nishomi.Persistence
{
    using Analystor.Nishomi.Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CustomerRequestConfiguration : NamedEntityConfiguration<CustomerRequest>
    {
        public override void Configure(EntityTypeBuilder<CustomerRequest> builder)
        {
            builder.HasOne(it => it.Product)
                   .WithMany(it => it.CustomerRequests)
                   .OnDelete(DeleteBehavior.Cascade);

            base.Configure(builder);
        }
    }
}
