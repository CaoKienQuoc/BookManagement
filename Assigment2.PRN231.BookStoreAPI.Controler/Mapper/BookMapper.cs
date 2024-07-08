using Assigment2.PRN231.BookStoreAPI.Repositories.Entity;
using Assigment2.PRN231.BookStoreAPI.Repositories.Models.Book;
using AutoMapper;

namespace Assigment2.PRN231.BookStoreAPI.Controlers.Mapper
{
    public class BookMapper : Profile
    {
        public BookMapper()
        {
            CreateMap<Book, BookRequestModel>().ReverseMap();
            CreateMap<Book, BookViewModel>().ReverseMap();
            CreateMap<Book, UpdateBookRequestModel>().ReverseMap();
        }
    }
}
