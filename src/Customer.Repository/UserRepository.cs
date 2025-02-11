using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CustomerManagement.Contracts;
using CustomerManagement.Contracts.Contracts;
using CustomerManagement.Repository.Models;
using Microsoft.Extensions.Logging;

namespace CustomerManagement.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;

        private ModelContext _modelContext;
        public UserRepository(ILogger<UserRepository> logger, ModelContext modelContext)
        {
            _logger = logger;
            _modelContext = modelContext;
        }

        public async Task<UserDetails> Getuser(int id)
        {
            var userdetails = new UserDetails();
            try
            {
                var userexists =await _modelContext.Users.FindAsync(id);
                if (userexists != null)
                {
                    userdetails = (from user in _modelContext.Users
                                   join userRole in _modelContext.UserRoles on user.Id equals userRole.UserId
                                   join role in _modelContext.Roles on userRole.RoleId equals role.RoleId
                                   select new UserDetails
                                   {
                                       Id = user.Id,
                                       Name = user.Name,
                                       Role = role.Role1
                                   }).FirstOrDefault();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return userdetails;
        }

        public async Task<UserDetails> GetuserDetails(LoginDetails loginDetails)
        {
            try
            {
                var user = _modelContext.Users.Where(e => e.Name.ToUpper().Equals(loginDetails.UserName) && e.Password.Equals(loginDetails.Password)).FirstOrDefault();
                if (user == null)
                {
                    throw new Exception("User not found");
                }
                var userdetails = new UserDetails
                {
                    Id = user.Id,
                    Name = user.Name,
                };
                return userdetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
