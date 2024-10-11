using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Application.AuthorOperations.Commands.DeleteAuthor;

public class DeleteAuthorCommandTests : IClassFixture<CommonTestFixture>
{
    public readonly BookStoreDbContext _context;

    public DeleteAuthorCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenNotExistAuthorIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
        command.AuthorId = 500;
        FluentActions.Invoking(() => command.Handle())
                        .Should().Throw<InvalidOperationException>()
                        .And.Message.Should().Be("Yazar bulunamadı.");
    }
    [Fact]
    public void WhenAuthorHasBooks_InvalidOperationException_ShouldBeReturn()
    {
        DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
        command.AuthorId = 3;
        FluentActions.Invoking(() => command.Handle())
                        .Should().Throw<InvalidOperationException>()
                        .And.Message.Should().Be("Yazarın kitabı mevcut, öncelikle kitaplar silinmelidir.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Author_ShouldBeDeleted()
    {
        DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
        command.AuthorId = 4;
        FluentActions.Invoking(() => command.Handle()).Invoke();
        var author = _context.Authors.SingleOrDefault(x => x.Id == command.AuthorId);
        author.Should().BeNull();
    }
}