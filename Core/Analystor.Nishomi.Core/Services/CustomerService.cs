namespace Analystor.Nishomi.Core
{
    using Analystor.Nishomi.Domain;
    using Analystor.Nishomi.Persistence;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// CustomerService
    /// </summary>
    public class CustomerService : ServiceBase, ICustomerRequest
    {
        public CustomerService(NishomiDbContextProvider contextProvider,ILogger<ServiceBase> logger):base(contextProvider,logger)
        {
        }

        /// <summary>
        /// Creates the customer request.
        /// </summary>
        /// <param name="customerReques">The customer reques.</param>
        /// <returns></returns>
        public bool CreateCustomerRequest(CustomerRequestDTO customerRequest)
        {
            CustomerRequest entry = new CustomerRequest()
            {
                Name= customerRequest.Name,
                Email= customerRequest.Email,
                ProductId= customerRequest.ProductId,
                ContactNumber= customerRequest.ContactNumber,
                Address= customerRequest.Address,
                Message= customerRequest.Message
            };

            this.CurrentDbContext.CustomerRequests.Add(entry);
            this.CurrentDbContext.SaveChanges();

            return true;
        }
    }
}
