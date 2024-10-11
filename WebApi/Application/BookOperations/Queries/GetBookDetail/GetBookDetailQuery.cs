using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.Queries.GetBookDetail;

public class GetBookDetailQuery
{
    public readonly IBookStoreDbContext _dbContext;
    public readonly IMapper _mapper;
    public int Id { get; set; }

    public GetBookDetailQuery(IBookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public GetBookDetailViewModel Handle()
    {
        var book = _dbContext.Books.Include(x => x.Genre).Include(x => x.Author).Where(book => book.Id == Id).SingleOrDefault();
        if (book is null)
        {
            throw new InvalidOperationException("Kitap bulunamadı.");
        }
        GetBookDetailViewModel vm = _mapper.Map<GetBookDetailViewModel>(book); //new GetBookDetailViewModel();
        // vm.Title = book.Title;
        // vm.PageCount = book.PageCount;
        // vm.Genre = ((GenreEnum)book.GenreId).ToString();
        // vm.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy");
        return vm;
    }
}

public class GetBookDetailViewModel
{
    public string Title { get; set; }
    public int PageCount { get; set; }
    public string Genre { get; set; }
    public string AuthorName { get; set; }
    public string PublishDate { get; set; }
}