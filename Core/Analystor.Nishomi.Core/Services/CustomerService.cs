namespace Analystor.Nishomi.Core
{
    using Analystor.Nishomi.Domain;
    using Analystor.Nishomi.Persistence;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Mail;
    using System.Text;
    using System.Web;

    /// <summary>
    /// CustomerService
    /// </summary>
    public class CustomerService : ServiceBase, ICustomerRequest
    {
        /// <summary>
        /// The product service
        /// </summary>
        private readonly IProduct _productService;

        /// <summary>
        /// The mail service
        /// </summary>
        private readonly IMailService _mailService;

        public CustomerService(NishomiDbContextProvider contextProvider,IProduct productService,IMailService mailService/*,ILogger<ServiceBase> logger*/):base(contextProvider/*,logger*/)
        {
            this._productService = productService;
            this._mailService = mailService;
        }

        /// <summary>
        /// Creates the customer request.
        /// </summary>
        /// <param name="customerReques">The customer reques.</param>
        /// <returns></returns>
        public string CreateCustomerRequest(CustomerRequestDTO customerRequest)
        {
            CustomerRequest entry = new CustomerRequest()
            {
                Name= customerRequest.Name,
                Email= customerRequest.Email,
                ProductId= customerRequest.ProductId,
                ContactNumber= customerRequest.ContactNumber,
                Address= customerRequest.Address,
                Message= customerRequest.Message,
                Street=customerRequest.Street,
                PostalCode=customerRequest.PostalCode,
                Country=customerRequest.Country
                
            };
            var count = this.CurrentDbContext.CustomerRequests.Count() + 1;
                this.SendCustomerEmail(customerRequest);
            this.CurrentDbContext.CustomerRequests.Add(entry);
            this.CurrentDbContext.SaveChanges();

            return customerRequest.IsOrder?"#niOmion" + count: "#niOmien" + count;
        }

        /// <summary>
        /// Gets the order number.
        /// </summary>
        /// <param name="customerRequest">The customer request.</param>
        /// <returns></returns>
        public string GetOrderNumber(CustomerRequestDTO customerRequest)
        {
            var count= this.CurrentDbContext.CustomerRequests.Count() + 1;
            return customerRequest.IsOrder ? "#niOmion" + count : "#niOmien" + count;
        }

        public void SendCustomerEmail(CustomerRequestDTO customerRequest)
        {
            var product = this._productService.GetProductDetails(customerRequest.ProductId);
            var count = this.CurrentDbContext.CustomerRequests.Count()+1;
            var orderNumber= customerRequest.IsOrder ? "#niOmion" + count : "#niOmien" + count;
            var requestBody = customerRequest.IsOrder ? "Thank you for your order on product “nishOmi -" + product.CategoryName + " " + product.ProductCode + "”.We will contact you as soon as your package ready for shipment.Please find the order summary details below." : "Thank you for your interest on product “nishOmi -" + product.CategoryName + " " + product.ProductCode + "”. We will contact you at the earliest, to know more about your enquiry. Please find the details of your enquiry";
            var street = customerRequest.Street != null ? customerRequest.Street + "<br/>" : "";
            var post = customerRequest.PostalCode != null ? customerRequest.PostalCode + "<br/>" : "";

            MailRequest mailRequest = new MailRequest()
            {
                ToEmail = customerRequest.Email,
                FromMail = "ahlan@nishomiabayas.com",
                Subject = customerRequest.IsOrder ? "nishOmi Abaya Order Confirmation" : "Acknowledgement : nishOmi Abaya product Enquiry",
                Body = "<html><body><img src ='https://nishomi-api.analystortech.com/Service/assets/nishomi.png' style='display: block;margin-left:auto;margin-right:auto;width:50% ' /><br/> " +
                    "Dear " +customerRequest.Name+"<br/><br/>"+
                    ""+requestBody+"<br/><br/>"+
                    "<u>Order Summary</u><br/>"+
                    "Order Number   &emsp;:&emsp;" + orderNumber+"<br/>"+
                    "Product Name   &emsp;:&emsp;nishomi - " + product.CategoryName + " " + product.ProductCode+"<br/>"+
                    "Product Description &emsp;:&emsp;" + product.Description+ "<br/>" +
                    "Product Size  &emsp;:&emsp;" + customerRequest.Size+"<br/>"+
                    "Order Total Price  &emsp;:&emsp;AED " + product.Cost.ToString("N2") + "<br/>" +
                    "Contact Address  &emsp;:<br/>" + street+post+customerRequest.Country+"<br/>"+
                    "Contact Number  &emsp;:&emsp;" + customerRequest.ContactNumber + "<br/>" +
                    "Email id  &emsp;:&emsp;" + customerRequest.Email + "<br/><br/>" +
                    "Thanks & Regards <br/>"+
                    "<b>Nishomi Designer Abayas</b>"+
                    "</body></html>",
            };
            _mailService.SendAckMail(mailRequest);
        }
    }
}
