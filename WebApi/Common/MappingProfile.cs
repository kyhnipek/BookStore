using AutoMapper;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.Entities;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;

namespace WebApi.Common;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateBookModel, Book>(); //CreateMap<Source,Target>();
        CreateMap<Book, GetBookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name)).ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => (src.Author.Name + " " + src.Author.Surname))).ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishDate.Date.ToString("dd/MM/yyyy")));
        CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name)).ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => (src.Author.Name + " " + src.Author.Surname))).ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishDate.Date.ToString("dd/MM/yyyy")));
        CreateMap<UpdateBookModel, Book>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != string.Empty));
        CreateMap<Genre, GenresViewModel>();
        CreateMap<Genre, GenreDetailViewModel>();
        CreateMap<UpdateGenreModel, Genre>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != string.Empty));
        CreateMap<Author, AuthorsViewModel>().ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday.Date.ToString("dd/MM/yyyy")));
        CreateMap<Author, AuthorDetailViewModel>().ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday.Date.ToString("dd/MM/yyyy")));
        CreateMap<CreateAuthorModel, Author>();
        CreateMap<UpdateAuthorModel, Author>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != string.Empty));
    }
}