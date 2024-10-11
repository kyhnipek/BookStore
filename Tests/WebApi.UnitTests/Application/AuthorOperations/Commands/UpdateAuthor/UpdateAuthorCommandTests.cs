using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.DBOperations;

namespace Application.AuthorOperations.Commands.UpdateAuthor;

public class UpdateAuthorCommandTests : IClassFixture<CommonTestFixture>

{
    public readonly BookStoreDbContext _context;
    public readonly IMapper _mapper;

    public UpdateAuthorCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }
    [Fact]
    public void WhenAuthorIsNotExist_InvalidOperationException_ShouldBeReturn()
    {
        UpdateAuthorCommand command = new UpdateAuthorCommand(_context, _mapper);
        command.AuthorId = 500;
        command.Model = new UpdateAuthorModel() { Name = "updateTest", Surname = "updateTest" };
        FluentActions.Invoking(() => command.Handle())
                        .Should().Throw<InvalidOperationException>()
                        .And.Message.Should().Be("Yazar bulunamadı.");
    }
    [Fact]
    public void WhenValidInputsAreGiven_Author_ShouldBeUpdated()
    {
        var model = new UpdateAuthorModel() { Name = "updateTest2", Surname = "updateTest2" };
        UpdateAuthorCommand command = new UpdateAuthorCommand(_context, _mapper);
        command.AuthorId = 2;
        command.Model = model;
        FluentActions.Invoking(() => command.Handle()).Invoke();
        var author = _context.Authors.SingleOrDefault(x => x.Id == command.AuthorId);
        author.Should().NotBeNull();
        author.Name.Should().Be(model.Name);
        author.Surname.Should().Be(model.Surname);
    }
}