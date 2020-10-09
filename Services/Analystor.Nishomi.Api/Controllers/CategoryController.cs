namespace Analystor.Nishomi.Api.Controllers
{
    using Analystor.Nishomi.Api.Filters;
    using Analystor.Nishomi.Core;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Serilog;
    using System;

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
        private readonly ILogger<CategoryController> _logger;


        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryController"/> class.
        /// </summary>
        public CategoryController(ICategory categoryService, ILogger<CategoryController> logger) //:base(logger)
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
        [HandleException("Unable to get categories")]
        public IActionResult GetCategories()
        {
            var categories = _categoryService.Categories();
            return Ok(categories, "Successfully retrieved categories.");
        }

        /// <summary>
        /// Gets the category products.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("CategoryProducts")]
        [HandleException("Unable to get category products")]
        public IActionResult GetCategoryProducts()
        {
                var catProducts = _categoryService.GetCategoryProducts();
                return Ok(catProducts, "Successfully retrieved Products.");
        }
    }
}