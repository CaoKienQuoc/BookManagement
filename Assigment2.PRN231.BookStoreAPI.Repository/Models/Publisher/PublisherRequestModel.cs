using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assigment2.PRN231.BookStoreAPI.Repositories.Models.Publisher
{
    public class PublisherRequestModel
    {
        public string PublisherName { get; set; } = null!;

        public string EmailAddress { get; set; } = null!;

        public string City { get; set; } = null!;

        public string State { get; set; } = null!;

        public string Country { get; set; } = null!;
    }
}
