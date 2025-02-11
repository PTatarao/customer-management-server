using CustomerManagement.Repository.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace customermanagement
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;

        private ModelContext _modelContext;
        public CustomerController(ILogger<CustomerController> logger, ModelContext modelContext)
        {
            _logger = logger;
            _modelContext = modelContext;
        }

        [HttpGet(Name = "GetCustomers")]
        public IEnumerable<CustomerDetails> Get()
        {
            var customers = _modelContext.Customers.ToList();
            return customers.Select(c => new CustomerDetails
            {
                dob = c.DateOfBirth,
                id = c.CustomerNumber,
                CustomerName = c.CustomerName,
                Gender = c.Gender,

            }).ToArray();

        }

        [HttpPost(Name = "CreateCustomer")]
        public async Task<IActionResult> Create([FromBody] CustomerDetails customerDetails)
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
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error whil creating customer");
            }

            return CreatedAtAction(nameof(Get), new { id = newCustomer.CustomerNumber }, newCustomer);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, CustomerDetails customerDetails)
        {
            if (id != customerDetails.id)
            {
                return BadRequest("Customer ID mismatch");
            }

            var existingCustomer = await _modelContext.Customers.FindAsync(id);
            if (existingCustomer == null)
            {
                return NotFound("Customer not found");
            }

            existingCustomer.CustomerName = customerDetails.CustomerName;
            existingCustomer.Gender = customerDetails.Gender;
            existingCustomer.DateOfBirth = customerDetails.dob;

            try
            {
                await _modelContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating product");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _modelContext.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound("customer not found");
            }

            _modelContext.Customers.Remove(customer);
            await _modelContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
