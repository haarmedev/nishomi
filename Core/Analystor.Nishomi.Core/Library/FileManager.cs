namespace Analystor.Nishomi.Core
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using System;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// FileManager
    /// </summary>
    /// <seealso cref="Analystor.Nishomi.Core.IFileManager" />
    public class FileManager : IFileManager
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<FileManager> _logger;

        /// <summary>
        /// The hosting environment
        /// </summary>
        protected readonly IHostingEnvironment _hostingEnvironment;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileManager" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="hostingEnvironment">The hosting environment.</param>
        public FileManager(ILogger<FileManager> logger, IHostingEnvironment hostingEnvironment)
        {
            this._logger = logger;
            this._hostingEnvironment = hostingEnvironment;
        }

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
        public async Task<string> CreateFileAsync(IFormFile file, string absolutePathPrefix, string relativePath, string fileName)
        {
            string path = string.Empty;

            try
            {
                var absolutePath = Path.Combine(this._hostingEnvironment.WebRootPath, absolutePathPrefix, relativePath);

                if (!Directory.Exists(absolutePath))
                {
                    Directory.CreateDirectory(absolutePath);
                }

                absolutePath = Path.Combine(absolutePath, fileName);

                if (File.Exists(absolutePath))
                {
                    File.Delete(absolutePath);
                }

                using (var fileStream = new FileStream(absolutePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                path = absolutePath;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Failed to create the file {@fileName}", fileName);

                throw ex;
            }

            return path;
        }

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
        public string CreateFileAsync(byte[] fileBuffer, string absolutePathPrefix, string relativePath, string fileName)
        {
            string path = string.Empty;

            try
            {
                var absolutePath = Path.Combine(this._hostingEnvironment.WebRootPath, absolutePathPrefix, relativePath);

                if (!Directory.Exists(absolutePath))
                {
                    Directory.CreateDirectory(absolutePath);
                }

                absolutePath = Path.Combine(absolutePath, fileName);

                if (File.Exists(absolutePath))
                {
                    File.Delete(absolutePath);
                }

                File.WriteAllBytes(absolutePath, fileBuffer);

                path = absolutePath;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Failed to create the file {@fileName}", fileName);

                throw ex;
            }

            return path;
        }
    }
}
