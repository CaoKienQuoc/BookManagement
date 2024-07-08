using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assigment2.PRN231.BookStoreAPI.Repositories.Models.Book
{
    public class BookModelResponse
    {
        public int total { get; set; }
        public int currentPage { get; set; }
        public List<BookViewModel> books { get; set; }
    }
}
