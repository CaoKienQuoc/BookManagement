using Assigment2.PRN231.BookStoreAPI.Repositories.Entity;
using Assigment2.PRN231.BookStoreAPI.Repositories.Models.BookAuthor;
using AutoMapper;

namespace Assigment2.PRN231.BookStoreAPI.Controlers.Mapper
{
    public class BookAuthorMapper : Profile
    {
        public BookAuthorMapper() 
        {
            CreateMap<BookAuthor,BookAuthorRequestModel>().ReverseMap();
            CreateMap<BookAuthor,BookAuthorViewModel>().ReverseMap();
        }
    }
}
