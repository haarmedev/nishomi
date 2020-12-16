namespace Analystor.Nishomi.Core
{
    /// <summary>
    /// ICustomerRequest
    /// </summary>
    public interface ICustomerRequest
    {
        /// <summary>
        /// Creates the customer request.
        /// </summary>
        /// <param name="customerReques">The customer reques.</param>
        /// <returns></returns>
        string CreateCustomerRequest(CustomerRequestDTO customerReques);
    }
}
