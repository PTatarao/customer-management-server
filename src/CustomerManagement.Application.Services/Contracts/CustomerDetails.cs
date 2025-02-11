using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Contracts.Contracts
{
    public class CustomerDetails
    {
        public DateOnly? dob { get; set; }

        public int id { get; set; }

        public string? Gender { get; set; }

        public string? CustomerName { get; set; }
    }
}
