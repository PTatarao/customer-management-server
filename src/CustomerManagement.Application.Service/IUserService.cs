using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerManagement.Contracts;
using CustomerManagement.Contracts.Contracts;

namespace CustomerManagement.Application.Service
{
    public interface IUserService
    {
        Task<UserDetails> Getuser(LoginDetails loginDetails);
    }
}
