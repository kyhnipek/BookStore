using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.Commands.DeleteBook;

public class DeleteBookCommand
{
    public IBookStoreDbContext _dbContext;
    public int Id { get; set; }
    public DeleteBookCommand(IBookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void Handle()
    {
        var book = _dbContext.Books.SingleOrDefault(x => x.Id == Id);
        if (book is null)
        {
            throw new InvalidOperationException("Kitap bulunamadı.");
        }
        _dbContext.Books.Remove(book);
        _dbContext.SaveChanges();
    }
}