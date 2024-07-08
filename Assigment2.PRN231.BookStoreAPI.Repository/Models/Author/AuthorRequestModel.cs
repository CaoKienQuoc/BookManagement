using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assigment2.PRN231.BookStoreAPI.Repositories.Models.Author
{
    public class AuthorRequestModel
    {
       

        public string LastName { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? Zip { get; set; }

        public string? EmailAddress { get; set; }
    }
}
