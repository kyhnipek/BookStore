using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.DBOperations;

namespace Application.BookOperations.Commands.DeleteBook;

public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public DeleteBookCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }
    [Fact]
    public void WhenNotExistBookIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        DeleteBookCommand command = new DeleteBookCommand(_context);
        command.Id = 500;
        FluentActions.Invoking(() => command.Handle())
                     .Should().Throw<InvalidOperationException>()
                     .And.Message.Should().Be("Kitap bulunamadı.");
    }
    [Fact]
    public void WhenValidInputIsGiven_Book_ShouldBeDeleted()
    {
        DeleteBookCommand command = new DeleteBookCommand(_context);
        command.Id = 1;
        FluentActions.Invoking(() => command.Handle()).Invoke();
        var book = _context.Books.SingleOrDefault(x => x.Id == 1);
        book.Should().BeNull();
    }
}