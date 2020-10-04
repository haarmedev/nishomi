namespace Analystor.Nishomi.Core
{
    using Analystor.Nishomi.Persistence;
    using Microsoft.Extensions.Logging;

    public class ServiceBase
    {
        /// <summary>
        /// The logger
        /// </summary>
        //protected readonly ILogger<ServiceBase> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceBase"/> class.
        /// </summary>
        /// <param name="contextProvider">The context provider.</param>
        /// <param name="logger">The logger.</param>
        public ServiceBase(NishomiDbContextProvider contextProvider /*ILogger<ServiceBase> logger*/)
        {
            this.ContextProvider = contextProvider;
            //this._logger = logger;
        }

        /// <summary>
        /// Gets the current database context.
        /// </summary>
        /// <value>
        /// The current database context.
        /// </value>
        public NishomiDbContext CurrentDbContext
        {
            get
            {
                return this.ContextProvider.GetCurrentDbContext();
            }
        }

        /// <summary>
        /// Gets the context provider.
        /// </summary>
        /// <value>
        /// The context provider.
        /// </value>
        protected NishomiDbContextProvider ContextProvider
        {
            get;
        }
    }
}
