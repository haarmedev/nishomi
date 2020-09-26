namespace Analystor.Nishomi.Domain
{
    using System;

    /// <summary>
    /// ProductImages
    /// </summary>
    /// <seealso cref="Analystor.Nishomi.Domain.Entity" />
    public class ProductImages : Entity
    {
        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        /// <value>
        /// The product identifier.
        /// </value>
        public Guid ProductId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        /// <value>
        /// The product.
        /// </value>
        public Product Product
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string Url
        {
            get;
            set;
        }
    }
}
