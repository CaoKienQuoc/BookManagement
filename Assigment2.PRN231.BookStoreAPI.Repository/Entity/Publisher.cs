using System;
using System.Collections.Generic;

namespace Assigment2.PRN231.BookStoreAPI.Repositories.Entity;

public partial class Publisher
{
    public int PubId { get; set; }

    public string PublisherName { get; set; } = null!;

    public string EmailAddress { get; set; } = null!;

    public string City { get; set; } = null!;

    public string State { get; set; } = null!;

    public string Country { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
