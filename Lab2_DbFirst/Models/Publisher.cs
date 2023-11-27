using System.Collections.Generic;

namespace Lab2_DbFirst.Models;

public partial class Publisher
{
    public int PublisherId { get; set; }

    public string? PublisherFirstName { get; set; }

    public string? PublisherLastName { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();

    public string? FullPublisherName => $"{PublisherFirstName} {PublisherLastName}";
}
