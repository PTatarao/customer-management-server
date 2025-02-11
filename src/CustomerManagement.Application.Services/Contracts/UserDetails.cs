using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Contracts.Contracts
{
    public class UserDetails
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Role { get; set; }
    }
}
