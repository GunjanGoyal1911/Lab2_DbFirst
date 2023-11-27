using System;
using System.Collections.Generic;

namespace Lab2_DbFirst.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string? CustomerFirstName { get; set; }

    public string? CustomerLastName { get; set; }

    public string? CustomerEmail { get; set; }

    public long? CustomerPhoneNumber { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
