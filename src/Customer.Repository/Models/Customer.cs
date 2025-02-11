using System;
using System.Collections.Generic;

namespace CustomerManagement.Repository.Models;

public partial class Customer
{
    public int CustomerNumber { get; set; }

    public string? CustomerName { get; set; }

    public string? Gender { get; set; }

    public DateOnly? DateOfBirth { get; set; }
}
