namespace Analystor.Nishomi.Api.Controllers
{
    using Analystor.Nishomi.Core;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System;

    /// <summary>
    /// ProductController.
    /// </summary>
    /// <seealso cref="Analystor.Nishomi.Api.Controllers.ApiController" />
    [Route("api/Product")]
    public class ProductController : ApiController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<ProductController> _logger;

        /// <summary>
        /// The product service
        /// </summary>
        private readonly IProduct _productService;

        /// <summary>
        /// The customer service
        /// </summary>
        private readonly ICustomerRequest _customerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductController"/> class.
        /// </summary>
        /// <param name="productService">The product service.</param>
        /// <param name="logger">The logger.</param>
        public ProductController(IProduct productService, ICustomerRequest customerService, ILogger<ProductController> logger) : base(logger)
        {
            this._logger = logger;
            this._productService = productService;
            this._customerService = customerService;
        }

        /// <summary>
        /// Gets the product details.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetProductDetails(Guid productId)
        {
            var productDetails = this._productService.GetProductDetails(productId);
            return Ok(productDetails, "Succesfully retrieved productdetails");
        }

        /// <summary>
        /// Gets the featured products.
        /// </summary>
        /// <returns></returns>
        [HttpGet("FeaturedProducts")]
        public IActionResult GetFeaturedProducts()
        {
            var products = this._productService.FeaturedProducts();
            return Ok(products, "Succesfully retrieved products");
        }

        /// <summary>
        /// Saves the customer request.
        /// </summary>
        /// <param name="details">The details.</param>
        /// <returns></returns>
        [HttpPost("SaveRequest")]
        public IActionResult SaveCustomerRequest([FromForm] CustomerRequestDTO details)
        {
            var status = this._customerService.CreateCustomerRequest(details);
            return Ok<object>(status, "Succsfully placed the request");
        }
    }
}