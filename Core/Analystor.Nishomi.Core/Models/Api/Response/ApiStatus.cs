namespace Analystor.Nishomi.Core
{
    using System.Net;

    /// <summary>
    /// Api Status
    /// </summary>
    /// <typeparam name="T">
    /// Any Type
    /// </typeparam>
    public class ApiStatus<T>
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public T Data
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public HttpStatusCode Status
        {
            get;
            set;
        }
    }
}
