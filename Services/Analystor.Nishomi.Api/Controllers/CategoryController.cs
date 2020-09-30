namespace Analystor.Nishomi.Api.Controllers
{
    using Analystor.Nishomi.Core;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    /// <summary>
    /// CategoryController.
    /// </summary>
    /// <seealso cref="Analystor.Nishomi.Api.Controllers.ApiController" />
    [Route("api/Category")]
    public class CategoryController : ApiController
    {
        /// <summary>
        /// The category service
        /// </summary>
        private readonly ICategory _categoryService;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<ApiController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryController"/> class.
        /// </summary>
        public CategoryController(ICategory categoryService,ILogger<ApiController> logger) : base(logger)
        {
            this._logger = logger;
            this._categoryService = categoryService;
        }

        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _categoryService.GetCategories();
            return Ok(categories, "Successfully retrieved categories.");
        }

        /// <summary>
        /// Gets the category products.
        /// </summary>
        /// <returns></returns>
        [HttpGet("CategoryProducts")]
        public IActionResult GetCategoryProducts()
        {
            var catProducts = _categoryService.GetCategoryProducts();
            return Ok(catProducts, "Successfully retrieved Products.");
        }
    }
}