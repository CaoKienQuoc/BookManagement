using System;
using System.Collections.Generic;

namespace Assigment2.PRN231.BookStoreAPI.Repositories.Entity;

public partial class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public string Type { get; set; } = null!;

    public int PubId { get; set; }

    public decimal Price { get; set; }

    public decimal? Advance { get; set; }

    public decimal? Royalty { get; set; }

    public decimal? YtdSales { get; set; }

    public string? Notes { get; set; }

    public DateTime? PublishedDate { get; set; }

    public virtual ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();

    public virtual Publisher Pub { get; set; } = null!;
}
