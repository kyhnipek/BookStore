using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.DBOperations;

namespace Application.GenreOperations.Commands.UpdateGenre;

public class UpdateGenreCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public UpdateGenreCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenNotExistGenreIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        UpdateGenreCommand command = new UpdateGenreCommand(_context, _mapper);
        command.GenreId = 500;
        command.Model = new UpdateGenreModel() { Name = "test", IsActive = true };
        FluentActions.Invoking(() => command.Handle())
                        .Should().Throw<InvalidOperationException>()
                        .And.Message.Should().Be("Kitap türü bulunamadı.");
    }
    [Fact]
    public void WhenValidInputsAreGiven_Genre_ShouldBeUpdated()
    {
        UpdateGenreCommand command = new UpdateGenreCommand(_context, _mapper);
        command.GenreId = 2;
        command.Model = new UpdateGenreModel() { Name = "testUpdate", IsActive = true };
        FluentActions.Invoking(() => command.Handle()).Invoke();
        var genre = _context.Genres.SingleOrDefault(x => x.Id == command.GenreId);
        genre.Name.Should().Be(command.Model.Name);
        genre.IsActive.Should().Be(command.Model.IsActive);
    }
}