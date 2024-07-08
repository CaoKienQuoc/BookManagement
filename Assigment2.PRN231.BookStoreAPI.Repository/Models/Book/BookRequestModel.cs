namespace Assigment2.PRN231.BookStoreAPI.Repositories.Models.Book
{
    public class BookRequestModel
    {


        public string Title { get; set; } = null!;

        public string Type { get; set; } = null!;
        public string PubName { get; set; } = null!;

        //public int PubId { get; set; }

        public decimal Price { get; set; }

        public decimal? Advance { get; set; }

        public decimal? Royalty { get; set; }

        public decimal? YtdSales { get; set; }

        public string? Notes { get; set; }
    }
}
