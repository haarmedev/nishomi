namespace Analystor.Nishomi.Persistence
{
    using Analystor.Nishomi.Domain;
    using Microsoft.EntityFrameworkCore;

    public class NishomiDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NishomiDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public NishomiDbContext(DbContextOptions<NishomiDbContext> options) : base(options)
        {

        }

        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <value>
        /// The categories.
        /// </value>
        public DbSet<Category> Categories => this.Set<Category>();

        /// <summary>
        /// Gets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        public DbSet<Product> Products => this.Set<Product>();

        /// <summary>
        /// Gets the product images.
        /// </summary>
        /// <value>
        /// The product images.
        /// </value>
        public DbSet<ProductImages> ProductImages => this.Set<ProductImages>();

        /// <summary>
        /// Gets the customer requests.
        /// </summary>
        /// <value>
        /// The customer requests.
        /// </value>
        public DbSet<CustomerRequest> CustomerRequests => this.Set<CustomerRequest>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            this.ConfigureEntities(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Configures the entities.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private void ConfigureEntities(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new CustomerRequestConfiguration());
            builder.ApplyConfiguration(new ProductImagesConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());
        }
    }
}
