using AutoMapper;
using BooksApi.DTO;
using BooksApi.Models;

namespace BooksApi
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Book, BookModel>().ReverseMap();
        }
    }
}
