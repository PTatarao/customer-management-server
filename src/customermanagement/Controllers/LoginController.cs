using System.Net;
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
        private ModelContext _modelContext;

        public LoginController(ILogger<LoginController> logger, ModelContext modelContext)
        {
            _logger = logger;
            _modelContext = modelContext;
        }
        [HttpPost(Name = "Login")]
        public IActionResult Login([FromBody] LoginDetails loginDetails)
        {
            try
            {
                bool userexists = _modelContext.Users.Any(e => e.Name.Equals(loginDetails.UserName) && e.Password.Equals(loginDetails.Password));
                if (userexists)
                {
                    var userdetails = (from user in _modelContext.Users
                                       join userRole in _modelContext.UserRoles on user.Id equals userRole.UserId
                                       join role in _modelContext.Roles on userRole.RoleId equals role.RoleId

                                       where user.Name.Equals(loginDetails.UserName) && user.Password.Equals(loginDetails.Password)
                                       select new UserDetails
                                       {
                                           Id = user.Id,
                                           Name = user.Name,
                                           Role = role.Role1
                                       }).FirstOrDefault();

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
