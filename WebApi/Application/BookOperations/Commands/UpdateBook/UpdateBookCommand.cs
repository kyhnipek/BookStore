using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.BookOperations.Commands.UpdateBook;

public class UpdateBookCommand
{
    public UpdateBookModel Model { get; set; }
    public IMapper _mapper;
    public int Id { get; set; }
    private readonly BookStoreDbContext _context;

    public UpdateBookCommand(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public void Handle()
    {
        var book = _context.Books.SingleOrDefault(x => x.Id == Id);
        if (book is null)
        {
            throw new InvalidOperationException("Kitap bulunamadı.");
        }
        _mapper.Map<UpdateBookModel, Book>(Model, book);
        // book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
        // // book.PageCount = Model.PageCount != default ? Model.PageCount : book.PageCount;
        // book.Title = Model.Title != default ? Model.Title : book.Title;
        // book.AuthorId = Model.AuthorId != default ? Model.AuthorId : book.AuthorId;
        // // book.PublishDate = Model.PublishDate != default ? Model.PublishDate : book.PublishDate;
        _context.SaveChanges();
    }
}
public class UpdateBookModel
{
    public string Title { get; set; }
    public int GenreId { get; set; }
    // public int PageCount { get; set; }
    public int AuthorId { get; set; }
    // public DateTime PublishDate { get; set; }
}