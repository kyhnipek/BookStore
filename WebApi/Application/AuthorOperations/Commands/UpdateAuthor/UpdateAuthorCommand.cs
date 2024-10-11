using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor;

public class UpdateAuthorCommand
{
    public UpdateAuthorModel Model { get; set; }
    public int AuthorId { get; set; }
    private readonly IBookStoreDbContext _context;
    private readonly IMapper _mapper;

    public UpdateAuthorCommand(IBookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Handle()
    {
        var author = _context.Authors.SingleOrDefault(c => c.Id == AuthorId);
        if (author is null)
            throw new InvalidOperationException("Yazar bulunamadı.");

        author = _mapper.Map<UpdateAuthorModel, Author>(Model, author);
        // author.Name = Model.Name != default ? Model.Name : author.Name;
        // author.Surname = Model.Surname != default ? Model.Surname : author.Surname;
        // author.Birthday = Model.Birthday != default ? Model.Birthday : author.Birthday;
        _context.SaveChanges();
    }
}

public class UpdateAuthorModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    // public DateTime Birthday { get; set; }
}