using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Queries.GetGenres;

public class GetGenresQuery
{
    public readonly IBookStoreDbContext _context;
    public readonly IMapper _mapper;
    public GetGenresQuery(IBookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public List<GenresViewModel> Handle()
    {
        var genres = _context.Genres.Where(x => x.IsActive).OrderBy(x => x.Id).ToList<Genre>();
        List<GenresViewModel> vm = _mapper.Map<List<GenresViewModel>>(genres);
        return vm;
    }
}

public class GenresViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
}