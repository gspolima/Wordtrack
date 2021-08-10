using AutoMapper;
using Wordtrack.Api.Dtos;
using Wordtrack.Domain;

namespace Wordtrack.Api.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookDto>();
            CreateMap<BookForCreationDto, Book>();
        }
    }
}
