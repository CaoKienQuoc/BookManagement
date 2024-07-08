using System;
using System.Collections.Generic;

namespace Assigment2.PRN231.BookStoreAPI.Repositories.Entity;

public partial class BookAuthor
{
    public int BookAuthorId { get; set; }

    public int BookId { get; set; }

    public int AuthorId { get; set; }

    public decimal? RoyaltyPercentage { get; set; }

    public virtual Author Author { get; set; } = null!;

    public virtual Book Book { get; set; } = null!;
}
