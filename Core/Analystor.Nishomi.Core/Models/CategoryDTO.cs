namespace Analystor.Nishomi.Core
{
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The CategoryDTO.
    /// </summary>
    public class CategoryDTO
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
        /// Gets or sets the name of the category.
        /// </summary>
        /// <value>
        /// The name of the category.
        /// </value>
        public string Name
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
        /// Gets or sets the file.
        /// </summary>
        /// <value>
        /// The file.
        /// </value>
        public IFormFile File
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the file1.
        /// </summary>
        /// <value>
        /// The file1.
        /// </value>
        public IFormFile ModelImage
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        public ICollection<ProductAPIDTO> Products
        {
            get;
            set;
        }
    }
}
