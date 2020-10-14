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
        ProductAPIDTO GetProductDetails(Guid ProductId);

        /// <summary>
        /// Saves the product image.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        bool SaveProductImage(Guid productId, string url);

        /// <summary>
        /// Creates the specified product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns></returns>
        bool Create(ProductDTO product);

        /// <summary>
        /// Updates the specified product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns></returns>
        bool Update(ProductDTO product);

        /// <summary>
        /// Deletes the specified product identifier.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <returns></returns>
        bool Delete(Guid productId);

        /// <summary>
        /// Featureds the products.
        /// </summary>
        /// <returns></returns>
        List<ProductAPIDTO> FeaturedProducts();

        /// <summary>
        /// Gets the products.
        /// </summary>
        /// <param name="totalRecords">The total records.</param>
        /// <param name="keyword">The keyword.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="sortHow">The sort how.</param>
        /// <returns></returns>
        List<ProductDetailsDTO> GetProducts(out int totalRecords, string keyword, int skip, int pageSize, string sortBy, string sortHow);

        /// <summary>
        /// Determines whether the specified name is duplicate.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="code">The code.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <c>true</c> if the specified name is duplicate; otherwise, <c>false</c>.
        /// </returns>
        bool IsDuplicate(string name, string code, Guid id);

        /// <summary>
        /// Products the detail.
        /// </summary>
        /// <param name="ProductId">The product identifier.</param>
        /// <returns></returns>
        ProductDTO ProductDetail(Guid ProductId);
    }
}
