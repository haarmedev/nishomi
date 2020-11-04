namespace Analystor.Nishomi.Api.Controllers
{
    using Analystor.Nishomi.Api.Filters;
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
        /// The mail service
        /// </summary>
        private readonly IMailService mailService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductController"/> class.
        /// </summary>
        /// <param name="productService">The product service.</param>
        /// <param name="logger">The logger.</param>
        public ProductController(IMailService mailService, IProduct productService, ICustomerRequest customerService/* ,ILogger<ProductController> logger*/) //: base(logger)
        {
            //this._logger = logger;
            this._productService = productService;
            this._customerService = customerService;
            this.mailService = mailService;
        }

        /// <summary>
        /// Gets the product details.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [HandleException("Unable to get product details")]
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
        [HandleException("Unable to get featured details")]
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
        [HandleException("Unable to get save request")]
        public IActionResult SaveCustomerRequest([FromForm] CustomerRequestDTO details)
        {
            var product = this._productService.GetProductDetails(details.ProductId);
            MailRequest mailRequest = new MailRequest()
            {
                ToEmail="fawaskallayi@gmail.com",
                Subject="Customer Requested Your Product",
                Body="<b>Product Details:<b><br/> Product Name: "+product.Name+"<br/> Product Cost: "+product.Cost.ToString("0.00")+ "<br/> Description: "+product.Description+" <br/> <b>Customer Details:<b><br/> Customer Name:"+details.Name+"<br/> Phone:"+details.ContactNumber+"<br/> Email:"+details.Email+" <br/> Address:"+details.Address+"<br/> Message:"+details.Message+"<br/> "+details.Size+"",
            };
            mailService.SendEmailAsync(mailRequest);
            var status = this._customerService.CreateCustomerRequest(details);
            return Ok<object>(status, "Succsfully placed the request");
        }
    }
}