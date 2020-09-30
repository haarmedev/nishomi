namespace Analystor.Nishomi.Core
{
    /// <summary>
    /// Represents class for success response
    /// </summary>
    /// <typeparam name="T">The Entity</typeparam>
    public class SuccessResponse<T> : ReponseBase
        where T : class
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
        /// Gets a value indicating whether this <see cref="SuccessResponse{T}"/> is success.
        /// </summary>
        /// <value>
        ///   <c>true</c> if success; otherwise, <c>false</c>.
        /// </value>
        public bool Success => true;
    }
}
