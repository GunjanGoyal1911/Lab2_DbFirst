using System;
using System.Collections.Generic;

namespace Lab2_DbFirst.Models;

public partial class Author
{
    public int AuthorId { get; set; }

    public string? AuthorFirstName { get; set; }

    public string? AuthorLastName { get; set; }

    public DateTime? AuthorBirthDate { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();

    public string? FullName => $"{AuthorFirstName} {AuthorLastName}";
}
