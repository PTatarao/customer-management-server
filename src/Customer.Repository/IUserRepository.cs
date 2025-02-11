using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerManagement.Contracts.Contracts;
using CustomerManagement.Contracts;

namespace CustomerManagement.Repository
{
 public   interface IUserRepository
    {
         Task<UserDetails> GetuserDetails(LoginDetails loginDetails);
    }
}
