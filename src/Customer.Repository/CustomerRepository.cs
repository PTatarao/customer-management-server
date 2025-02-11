using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerManagement.Contracts.Contracts;
using CustomerManagement.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CustomerManagement.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ILogger<CustomerRepository> _logger;

        private ModelContext _modelContext;
        public CustomerRepository(ILogger<CustomerRepository> logger, ModelContext modelContext)
        {
            _logger = logger;
            _modelContext = modelContext;
        }
        public async Task CreateCustomers(CustomerDetails customerDetails)
        {
            var newCustomer = new CustomerManagement.Repository.Models.Customer
            {
                DateOfBirth = customerDetails?.dob,
                CustomerName = customerDetails?.CustomerName,
                Gender = customerDetails?.Gender,

            };
            _modelContext.Customers.Add(newCustomer);
            try
            {
                await _modelContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteCustomers(int id)
        {
            var customer = await _modelContext.Customers.FindAsync(id);
            if (customer == null)
            {
                throw new Exception("Customer not found");
            }
            try
            {
                _modelContext.Customers.Remove(customer);
                await _modelContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CustomerDetails>> GetCustomers()
        {
            var customers = await _modelContext.Customers.ToListAsync();
            return customers.Select(c => new CustomerDetails
            {
                dob = c.DateOfBirth,
                id = c.CustomerNumber,
                CustomerName = c.CustomerName,
                Gender = c.Gender,

            }).ToArray();
        }

        public async Task UpdateCustomer(int id, CustomerDetails customerDetails)
        {
            var existingCustomer = await _modelContext.Customers.FindAsync(id);
            if (existingCustomer == null)
            {
                throw new Exception("Customer not found");
            }

            existingCustomer.CustomerName = customerDetails.CustomerName;
            existingCustomer.Gender = customerDetails.Gender;
            existingCustomer.DateOfBirth = customerDetails.dob;

            try
            {
                await _modelContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
