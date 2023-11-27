using System;

namespace Lab2_DbFirst.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? CustomerId { get; set; }

    public DateTime? OrderDate { get; set; }

    public string? OrderStatus { get; set; }

    public int? StoreId { get; set; }

    public string? Isbn13 { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Book? Isbn13Navigation { get; set; }

    public virtual Store? Store { get; set; }
}
