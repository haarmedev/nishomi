namespace Analystor.Nishomi.Core
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// ICategory
    /// </summary>
    public interface ICategory
    {
        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <returns></returns>
        List<CategoryDTO> GetCategories(out int totalRecords, string keyword, int skip, int pageSize, string sortBy, string sortHow);

        /// <summary>
        /// Gets the category products.
        /// </summary>
        /// <returns></returns>
        List<CategoryDTO> GetCategoryProducts();

        /// <summary>
        /// Creates the specified category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns></returns>
        bool Create(CategoryDTO category);

        /// <summary>
        /// Updates the specified category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns></returns>
        bool Update(CategoryDTO category);

        /// <summary>
        /// Deletes the specified category identifier.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns></returns>
        bool Delete(Guid categoryId);

        /// <summary>
        /// Gets the category.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns></returns>
        CategoryDTO GetCategory(Guid categoryId);

        /// <summary>
        /// Categorieses this instance.
        /// </summary>
        /// <returns></returns>
        List<CategoryDTO> Categories();
    }
}
