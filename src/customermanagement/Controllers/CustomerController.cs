using System.Net;
using CustomerManagement.Application.Service;
using CustomerManagement.Contracts.Contracts;
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
        private readonly ICustomerService _customerService;

        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        [HttpGet(Name = "GetCustomers")]
        public async Task<ActionResult> Get()
        {
            var customers = await _customerService.GetCustomers();
            return Ok(customers);

        }

        [HttpPost(Name = "CreateCustomer")]
        public async Task<IActionResult> Create([FromBody] CustomerDetails customerDetails)
        {
            try
            {
               await _customerService.CreateCustomers(customerDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error whil creating customer");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error whil creating customer");
            }

            return Ok();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, CustomerDetails customerDetails)
        {
            if (id != customerDetails.id)
            {
                return BadRequest("Customer ID mismatch");
            }
            try
            {
                await _customerService.UpdateCustomers(customerDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating customer");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while updating customer");
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            if (id==0)
            {
                return BadRequest("Customer ID should be greater than 0");
            }
            try
            {
                await _customerService.DeleteCustomers(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting customer");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while deleting customer");
            }
            return Ok();
        }
    }
}
