namespace Analystor.Nishomi.Core
{
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// IFileManager
    /// </summary>
    public interface IFileManager
    {
        /// <summary>
        /// Creates the file asynchronous.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="absolutePathPrefix">The absolute path prefix.</param>
        /// <param name="relativePath">The relative path.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        /// The Uri
        /// </returns>
        Task<string> CreateFileAsync(IFormFile file, string absolutePathPrefix, string relativePath, string fileName);

        /// <summary>
        /// Creates the file asynchronous.
        /// </summary>
        /// <param name="fileBuffer">The file buffer.</param>
        /// <param name="absolutePathPrefix">The absolute path prefix.</param>
        /// <param name="relativePath">The relative path.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        /// The Uri
        /// </returns>
        string CreateFileAsync(byte[] fileBuffer, string absolutePathPrefix, string relativePath, string fileName);
    }
}
