using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerManagement.Contracts.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.Application.Service
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDetails>> GetCustomers();
        Task CreateCustomers(CustomerDetails customer);
        Task DeleteCustomers(int id);
        Task UpdateCustomers(CustomerDetails customer);
    }
}
