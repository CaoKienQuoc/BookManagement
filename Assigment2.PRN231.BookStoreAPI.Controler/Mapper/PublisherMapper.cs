using Assigment2.PRN231.BookStoreAPI.Repositories.Entity;
using Assigment2.PRN231.BookStoreAPI.Repositories.Models.Publisher;
using AutoMapper;

namespace Assigment2.PRN231.BookStoreAPI.Controlers.Mapper
{
    public class PublisherMapper : Profile
    {
        public PublisherMapper() 
        {
            CreateMap<Publisher, PublisherViewModel>().ReverseMap();
            CreateMap<Publisher, PublisherRequestModel>().ReverseMap();
        }
    }
}
