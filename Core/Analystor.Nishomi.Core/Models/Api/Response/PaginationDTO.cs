namespace Analystor.Nishomi.Core
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Pagination DTO
    /// </summary>
    /// <typeparam name="T">The Entity</typeparam>
    /// <seealso cref="System.Collections.ObjectModel.Collection{T}" />
    public class PaginationDTO<T> :
        Collection<T>
        where T : class
    {
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public List<T> Items
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the total.
        /// </summary>
        /// <value>
        /// The total.
        /// </value>
        public int Total
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the page number.
        /// </summary>
        /// <value>
        /// The page number.
        /// </value>
        public int PageNumber
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the total items in page.
        /// </summary>
        /// <value>
        /// The total items in page.
        /// </value>
        public int TotalItemsInPage
        {
            get;
            set;
        }
    }
}
