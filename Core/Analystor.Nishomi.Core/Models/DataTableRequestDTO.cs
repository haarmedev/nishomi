namespace Analystor.Nishomi.Core
{
    /// <summary>
    /// Represents data table request
    /// </summary>
    public class DataTableRequest
    {
        /// <summary>
        /// Gets or sets the draw.
        /// </summary>
        /// <value>
        /// The draw.
        /// </value>
        public int Draw
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the start.
        /// </summary>
        /// <value>
        /// The start.
        /// </value>
        public int Skip
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        public int PageSize
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the sort column.
        /// </summary>
        /// <value>
        /// The sort column.
        /// </value>
        public string SortColumn
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the sort column direction.
        /// </summary>
        /// <value>
        /// The sort column direction.
        /// </value>
        public string SortColumnDirection
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the search value.
        /// </summary>
        /// <value>
        /// The search value.
        /// </value>
        public string SearchValue
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the additional data.
        /// </summary>
        /// <value>
        /// The additional data.
        /// </value>
        public dynamic AdditionalData
        {
            get;
            set;
        }
    }
}
