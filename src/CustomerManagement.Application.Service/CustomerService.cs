using CustomerManagement.Contracts.Contracts;
using CustomerManagement.Repository.Models;
using CustomerManagement.Repository;
using Microsoft.Extensions.Logging;

namespace CustomerManagement.Application.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ILogger<CustomerService> _logger;
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ILogger<CustomerService> logger,ICustomerRepository customerRepository)
        {
            _customerRepository=customerRepository;
            _logger = logger;
        }
        public async Task CreateCustomers(CustomerDetails customer)
        {
         await _customerRepository.CreateCustomers(customer);
        }

        public async Task DeleteCustomers(int id)
        {
         await  _customerRepository.DeleteCustomers(id);
        }

        public async Task<IEnumerable<CustomerDetails>> GetCustomers()
        {
           return await _customerRepository.GetCustomers();
        }

        public async Task UpdateCustomers(CustomerDetails customer)
        {
           await _customerRepository.UpdateCustomer(customer.id, customer);
        }
    }
}
