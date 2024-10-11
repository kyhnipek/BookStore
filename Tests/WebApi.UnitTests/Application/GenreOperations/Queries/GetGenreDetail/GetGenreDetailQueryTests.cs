using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Application.GenreOperations.Queries.GetGenreDetail;

public class GetGenreDetailQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetGenreDetailQueryTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenNotExistGenreIsGiven_InvalidOperationExpception_ShouldBeReturn()
    {
        GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
        query.Id = 500;
        FluentActions.Invoking(() => query.Handle())
                        .Should().Throw<InvalidOperationException>()
                        .And.Message.Should().Be("Kitap türü bulunamadı.");
    }
    [Fact]
    public void WhenValidInputsAreGiven_Genre_ShouldBeReturn()
    {
        var genre = _context.Genres.SingleOrDefault(x => x.Id == 1);
        GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
        query.Id = genre.Id;
        GenreDetailViewModel model = query.Handle();
        model.Name.Should().Be(genre.Name);
    }
}