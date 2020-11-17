using System.Collections.Generic;

namespace Analystor.Nishomi.Domain
{
    /// <summary>
    /// Category
    /// </summary>
    /// <seealso cref="Analystor.Nishomi.Domain.NamedEntity" />
    public class Category : NamedEntity
    {
        /// <summary>
        /// Gets or sets the name in arabic.
        /// </summary>
        /// <value>
        /// The name in arabic.
        /// </value>
        public string NameAr
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the caption.
        /// </summary>
        /// <value>
        /// The caption.
        /// </value>
        public string Caption
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

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>
        /// The image.
        /// </value>
        public string Image
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        public ICollection<Product> Products
        {
            get;
        }
    }
}
