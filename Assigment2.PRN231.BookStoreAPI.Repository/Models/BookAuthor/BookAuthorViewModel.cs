using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assigment2.PRN231.BookStoreAPI.Repositories.Models.BookAuthor
{
    public class BookAuthorViewModel
    {
        public int BookAuthorId { get; set; }

        public int BookId { get; set; }

        public int AuthorId { get; set; }

        public decimal? RoyaltyPercentage { get; set; }

    }
}
