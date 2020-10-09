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
        /// Determines whether the specified name is duplicate.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <c>true</c> if the specified name is duplicate; otherwise, <c>false</c>.
        /// </returns>
        bool IsDuplicate(string name, Guid id);

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
