namespace Assigment2.PRN231.BookStoreAPI.Repositories.Models.Book
{
    public class UpdateBookRequestModel
    {
        public string Title { get; set; } = null!;

        public string Type { get; set; } = null!;

        public decimal Price { get; set; }

        public decimal? Advance { get; set; }


    }
}
