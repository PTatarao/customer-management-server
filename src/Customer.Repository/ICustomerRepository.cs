using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerManagement.Contracts.Contracts;

namespace CustomerManagement.Repository
{
   public interface ICustomerRepository
    {
        Task<IEnumerable<CustomerDetails>> GetCustomers();
        Task CreateCustomers(CustomerDetails customerDetails);
        Task DeleteCustomers(int id);
        Task UpdateCustomer(int id, CustomerDetails customerDetails);
    }
}
