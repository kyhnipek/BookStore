using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor;

public class CreateAuthorCommand
{
    public CreateAuthorModel Model { get; set; }
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public CreateAuthorCommand(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Handle()
    {
        var author = _context.Authors.SingleOrDefault(x => x.Name.ToLower() == Model.Name.ToLower() && x.Surname.ToLower() == Model.Surname.ToLower());
        if (author is not null)
            throw new InvalidOperationException("Yazar zaten mevcut.");

        author = _mapper.Map<Author>(Model);
        _context.Add(author);
        _context.SaveChanges();
    }
}

public class CreateAuthorModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime Birthday { get; set; }

}