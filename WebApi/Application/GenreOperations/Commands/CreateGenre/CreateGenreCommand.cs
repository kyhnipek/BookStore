using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Commands.CreateGenre;

public class CreateGenreCommand
{
    public CreateGenreModel Model { get; set; }
    public readonly IBookStoreDbContext _context;

    public CreateGenreCommand(IBookStoreDbContext context)
    {
        _context = context;
    }
    public void Handle()
    {
        var genre = _context.Genres.SingleOrDefault(x => x.Name == Model.Name);
        if (genre is not null)
            throw new InvalidOperationException("Kitap türü zaten mevcut.");

        genre = new Genre();
        genre.Name = Model.Name;
        _context.Add(genre);
        _context.SaveChanges();
    }
}
public class CreateGenreModel
{
    public string Name { get; set; }
}