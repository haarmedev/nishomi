using System;
using System.Collections.Generic;
using System.Text;

namespace Analystor.Nishomi.Core
{
    public class ProductDetailsDTO
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
        /// Gets or sets the product code.
        /// </summary>
        /// <value>
        /// The product code.
        /// </value>
        public string ProductCode
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the product code in arabic.
        /// </summary>
        /// <value>
        /// The product code in arabic.
        /// </value>
        public string ProductCodeAr
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        /// <value>
        /// The name of the product.
        /// </value>
        public string ProductName
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
        /// Gets or sets the description in Arabic.
        /// </summary>
        /// <value>
        /// The description in Arabic.
        /// </value>
        public string DescriptionAr
        {
            get;
            set;
        }
    }
}
