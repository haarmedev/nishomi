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
            details.CategoryName = product.CategoryName;
            var request = details.IsOrder ? "<b>product order</b>" : "<b>product Interest</b>";
            var street = details.Street != null ? details.Street + "<br/>" : "";
            var post = details.PostalCode != null ? details.PostalCode + "<br/>" : "";
            MailRequest mailRequest = new MailRequest()
            {
                ToEmail="ahlan@nishomiabayas.com",
                FromMail= "ahlan.nishomiabayas@outlook.com",
                Subject =details.IsOrder? "Customer Placed a Product Order" : "Customer Placed a Product Interest",
                Body="<b>Hi,</b> </br>"+
                     "Please see below the details of a "+request+ " from customer<br/>"+
                     "Order Number&emsp;:&emsp;" + _customerService.GetOrderNumber(details)+"<br/>"+
                     "<b><i><u>Product Details</u></i></b><br/><br/>"+
                     "Product Name&emsp;:&emsp;nishOmi - " + product.CategoryName+" "+product.ProductCode+"<br/>"+
                     "Product Cost&emsp;:&emsp;AED " + product.Cost.ToString("N2") + "<br/>"+
                     "Product Description&emsp;:&emsp;" + product.Description+"<br/><br/>"+
                     "<b><i><u>Customer Details</u></i></b><br/><br/>" +
                     "Customer Name&emsp;:&emsp;" + details.Name+" <br/>"+
                     "Customer Phone&emsp;:&emsp;" + details.ContactNumber+ " <br/>" +
                     "Customer Email&emsp;:&emsp;" + details.Email+ " <br/>" +
                     "Customer Address&emsp;:<br/>" +street+post+ details.Country+"<br/>" +
                     "Customer Size&emsp;:&emsp;" + details.Size+" <br/>" +
                     "Customer Note&emsp;:&emsp;" + details.Message,
            };
            mailService.SendEmailAsync(mailRequest);
            var status = this._customerService.CreateCustomerRequest(details);
            return Ok<object>(status, "Succsfully placed the request");
        }
    }
}