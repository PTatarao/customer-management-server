using System.Net;
using CustomerManagement.Application.Service;
using CustomerManagement.Contracts;
using CustomerManagement.Contracts.Contracts;
using CustomerManagement.Repository.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace customermanagement.Controllers
{
    [Route("api/customer/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IUserService _userService;
        public LoginController(ILogger<LoginController> logger, IUserService userService)
        {
            _userService = userService;
            _logger = logger;
        }
        [HttpPost(Name = "Login")]
        public async Task<IActionResult> Login([FromBody] LoginDetails loginDetails)
        {
            try
            {
                var userdetails = await _userService.Getuser(loginDetails);
                if (userdetails != null)
                {
                    return Ok(userdetails);
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.Unauthorized);
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
