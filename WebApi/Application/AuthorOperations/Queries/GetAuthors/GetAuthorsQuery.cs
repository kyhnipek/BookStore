using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthors;

public class GetAuthorsQuery
{
    private readonly IBookStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetAuthorsQuery(IBookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public List<AuthorsViewModel> Handle()
    {
        var authorList = _context.Authors.OrderBy(x => x.Id).ToList<Author>();
        List<AuthorsViewModel> vm = _mapper.Map<List<AuthorsViewModel>>(authorList);
        return vm;
    }
}

public class AuthorsViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Birthday { get; set; }
}