using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Contracts.Contracts
{
    public class CustomerDetails
    {
        public int CustomerNumber { get; set; }

        public string? CustomerName { get; set; }

        public string? Gender { get; set; }

        public DateOnly? DateOfBirth { get; set; }
    }
}
