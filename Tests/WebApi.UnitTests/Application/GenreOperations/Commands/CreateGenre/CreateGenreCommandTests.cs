using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Application.GenreOperations.Commands.CreateGenre;

public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;

    public CreateGenreCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenExistGenreIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        var genre = new Genre() { Name = "test" };
        _context.Genres.Add(genre);
        _context.SaveChanges();

        CreateGenreCommand command = new CreateGenreCommand(_context);
        command.Model = new CreateGenreModel() { Name = genre.Name };
        FluentActions.Invoking(() => command.Handle())
                        .Should().Throw<InvalidOperationException>()
                        .And.Message.Should().Be("Kitap türü zaten mevcut.");
    }
    [Fact]
    public void WhenValidInputsAreGiven_Genre_ShouldBeCreated()
    {
        string genreName = "testCreate";
        CreateGenreCommand command = new CreateGenreCommand(_context);
        command.Model = new CreateGenreModel() { Name = genreName };
        FluentActions.Invoking(() => command.Handle()).Invoke();
        var genre = _context.Genres.SingleOrDefault(x => x.Name == genreName);
        genre.Should().NotBeNull();
        genre.Name.Should().Be(genreName);
    }
}