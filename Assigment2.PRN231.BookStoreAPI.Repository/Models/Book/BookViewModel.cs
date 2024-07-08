namespace Assigment2.PRN231.BookStoreAPI.Repositories.Models.Book
{
    public class BookViewModel
    {
        public int BookId { get; set; }

        public string Title { get; set; } = null!;

        public string Type { get; set; } = null!;



        public decimal Price { get; set; }

        public decimal? Advance { get; set; }


        public virtual PublisherModel Publisher { get; set; }
        public class PublisherModel
        {
            public string Name { get; set; }
        }
    }
}
