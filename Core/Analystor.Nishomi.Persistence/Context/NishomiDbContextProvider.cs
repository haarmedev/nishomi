namespace Analystor.Nishomi.Persistence
{
    using Microsoft.AspNetCore.Http;

    public class NishomiDbContextProvider
    {
        /// <summary>
        /// The service provider
        /// </summary>
        private readonly IHttpContextAccessor _contextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="EniticsDbContextProvider" /> class.
        /// </summary>
        /// <param name="contextAccessor">The context accessor.</param>
        public NishomiDbContextProvider(IHttpContextAccessor contextAccessor)
        {
            this._contextAccessor = contextAccessor;
        }

        /// <summary>
        /// Creates the database context.
        /// </summary>
        /// <returns>
        /// The db context.
        /// </returns>
        public NishomiDbContext GetCurrentDbContext()
        {
            var context = (NishomiDbContext)this._contextAccessor.HttpContext.RequestServices.GetService(typeof(NishomiDbContext));

            return context;
        }
    }
}
