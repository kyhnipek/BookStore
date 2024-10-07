using WebApi.DbOperations;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor;

public class DeleteAuthorCommand
{
    public int AuthorId { get; set; }
    private readonly BookStoreDbContext _context;

    public DeleteAuthorCommand(BookStoreDbContext context)
    {
        _context = context;
    }

    public void Handle()
    {
        var author = _context.Authors.SingleOrDefault(c => c.Id == AuthorId);
        if (author is null)
            throw new InvalidOperationException("Yazar bulunamadı.");
        var authorBook = _context.Books.Any(c => c.AuthorId == AuthorId);
        if (authorBook is true)
            throw new InvalidOperationException("Yazarın kitabı mevcut, öncelikle kitaplar silinmelidir.");
        _context.Remove(author);
        _context.SaveChanges();
    }
}