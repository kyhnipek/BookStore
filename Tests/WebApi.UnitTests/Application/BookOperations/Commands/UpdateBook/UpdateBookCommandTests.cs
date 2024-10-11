using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DBOperations;

namespace Application.BookOperations.Commands.UpdateBook;

public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;
    public int Id { get; set; }

    public UpdateBookCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenNotExistBookIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        UpdateBookCommand command = new UpdateBookCommand(_context, _mapper);
        command.Id = 500;
        FluentActions.Invoking(() => command.Handle())
                     .Should().Throw<InvalidOperationException>()
                     .And.Message.Should().Be("Kitap bulunamadı.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Book_ShouldBeUpdated()
    {
        UpdateBookCommand command = new UpdateBookCommand(_context, _mapper);
        var model = new UpdateBookModel() { Title = "Test", AuthorId = 2, GenreId = 3 };
        command.Id = 2;
        command.Model = model;
        FluentActions.Invoking(() => command.Handle()).Invoke();
        var book = _context.Books.SingleOrDefault(b => b.Id == command.Id);
        book.AuthorId.Should().Be(model.AuthorId);
        book.GenreId.Should().Be(model.GenreId);
        book.Title.Should().Be(model.Title);
    }
}