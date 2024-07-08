using Assigment2.PRN231.BookStoreAPI.Repositories.Entity;
using Assigment2.PRN231.BookStoreAPI.Repositories.Models.Author;
using AutoMapper;

namespace Assigment2.PRN231.BookStoreAPI.Controlers.Mapper
{
    public class AuthorMapper : Profile
    {
        public AuthorMapper()
        {
            CreateMap<Author, AuthorViewModel>().ReverseMap();
            CreateMap<Author, AuthorRequestModel>().ReverseMap();
            
        }
    }
}
