namespace Analystor.Nishomi.Domain
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Product
    /// </summary>
    /// <seealso cref="Analystor.Nishomi.Domain.NamedEntity" />
    public class Product : NamedEntity
    {
        /// <summary>
        /// Gets or sets the category identifier.
        /// </summary>
        /// <value>
        /// The category identifier.
        /// </value>
        public Guid CategoryId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        public Category Category
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        public string Color
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the cost.
        /// </summary>
        /// <value>
        /// The cost.
        /// </value>
        public decimal Cost
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the product images.
        /// </summary>
        /// <value>
        /// The product images.
        /// </value>
        public ICollection<ProductImages> ProductImages
        {
            get;
        }

        /// <summary>
        /// Gets the customer requests.
        /// </summary>
        /// <value>
        /// The customer requests.
        /// </value>
        public ICollection<CustomerRequest> CustomerRequests
        {
            get;
        }
    }
}
