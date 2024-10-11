using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.DBOperations;

namespace Application.GenreOperations.Commands.DeleteGenre;

public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;

    public DeleteGenreCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenNotExistGenreIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        DeleteGenreCommand command = new DeleteGenreCommand(_context);
        command.GenreId = 500;
        FluentActions.Invoking(() => command.Handle())
                        .Should().Throw<InvalidOperationException>()
                        .And.Message.Should().Be("Kitap türü bulunamadı.");
    }
    [Fact]
    public void WhenValidInputsAreGiven_Genre_ShouldBeDeleted()
    {
        DeleteGenreCommand command = new DeleteGenreCommand(_context);
        command.GenreId = 3;
        FluentActions.Invoking(() => command.Handle()).Invoke();
        var genre = _context.Genres.SingleOrDefault(x => x.Id == command.GenreId);
        genre.Should().BeNull();
    }
}