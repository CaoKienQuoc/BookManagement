using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assigment2.PRN231.BookStoreAPI.Repositories.Models.BookAuthor
{
    public class BookAuthorRequestModel
    {
        

        public int BookId { get; set; }

        public int AuthorId { get; set; }

        public decimal? RoyaltyPercentage { get; set; }

    }
}
