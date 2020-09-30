namespace Analystor.Nishomi.Core
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// IProduct
    /// </summary>
    public interface IProduct
    {
        /// <summary>
        /// Gets the product details.
        /// </summary>
        /// <param name="ProductId">The product identifier.</param>
        /// <returns></returns>
        ProductDTO GetProductDetails(Guid ProductId);

        /// <summary>
        /// Saves the product image.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        bool SaveProductImage(Guid productId, string url);

        /// <summary>
        /// Featureds the products.
        /// </summary>
        /// <returns></returns>
        List<ProductDTO> FeaturedProducts();
    }
}
