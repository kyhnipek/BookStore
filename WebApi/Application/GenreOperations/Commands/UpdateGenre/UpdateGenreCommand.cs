using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre;

public class UpdateGenreCommand
{
    public int GenreId { get; set; }
    private readonly BookStoreDbContext _context;
    public readonly IMapper _mapper;
    public UpdateGenreModel Model { get; set; }

    public UpdateGenreCommand(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public void Handle()
    {
        var genre = _context.Genres.SingleOrDefault(x => x.Id == GenreId);
        if (genre is null)
            throw new InvalidOperationException("Kitap türü bulunamadı.");
        if (_context.Genres.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.Id != GenreId))
            throw new InvalidOperationException("Kitap türü zaten mevcut");
        genre.Name = string.IsNullOrEmpty(Model.Name.ToLower()) ? genre.Name : Model.Name;
        genre.IsActive = Model.IsActive;
        _context.SaveChanges();
    }
}

public class UpdateGenreModel
{
    public string Name { get; set; }
    public bool IsActive { get; set; }
}