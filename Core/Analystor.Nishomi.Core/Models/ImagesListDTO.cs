namespace Analystor.Nishomi.Core
{
    /// <summary>
    /// ImagesListDTO
    /// </summary>
    public class ImagesListDTO
    {
        /// <summary>
        /// Gets or sets the image URL.
        /// </summary>
        /// <value>
        /// The image URL.
        /// </value>
        public string ImageUrl
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is main image.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is main image; otherwise, <c>false</c>.
        /// </value>
        public bool IsMainImage
        {
            get;
            set;
        }
    }
}
