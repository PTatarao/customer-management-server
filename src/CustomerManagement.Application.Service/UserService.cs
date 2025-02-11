using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerManagement.Contracts;
using CustomerManagement.Contracts.Contracts;
using CustomerManagement.Repository;
using Microsoft.Extensions.Logging;

namespace CustomerManagement.Application.Service
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _userRepository;
        public UserService(ILogger<UserService> logger, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _logger = logger;
        }
        public async Task<UserDetails> Getuser(LoginDetails loginDetails)
        {
          return await _userRepository.GetuserDetails(loginDetails);
        }
    }
}
