using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;

public class GetAuthorDetailQuery
{
    public int AuthorId { get; set; }
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetAuthorDetailQuery(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public AuthorDetailViewModel Handle()
    {
        var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);
        if (author is null)
            throw new InvalidOperationException("Yazar bulunamadı.");

        AuthorDetailViewModel vm = _mapper.Map<AuthorDetailViewModel>(author);

        return vm;
    }
}

public class AuthorDetailViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Birthday { get; set; }
}