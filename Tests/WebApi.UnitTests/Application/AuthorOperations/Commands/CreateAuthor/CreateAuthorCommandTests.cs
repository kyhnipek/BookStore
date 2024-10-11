using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Application.AuthorOperations.Commands.CreateAuthor;

public class CreateAuthorCommandTests : IClassFixture<CommonTestFixture>
{
    public readonly BookStoreDbContext _context;
    public readonly IMapper _mapper;

    public CreateAuthorCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }
    [Fact]
    public void WhenAuthorIsExist_InvalidOperationException_ShouldBeReturn()
    {
        var author = new Author() { Name = "createAuthorTestName", Surname = "createAuthorTestSurname", Birthday = new DateTime(1969, 12, 31) };
        _context.Authors.Add(author);
        _context.SaveChanges();
        CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
        command.Model = new CreateAuthorModel() { Name = author.Name, Surname = author.Surname, Birthday = author.Birthday };
        FluentActions.Invoking(() => command.Handle())
                        .Should().Throw<InvalidOperationException>()
                        .And.Message.Should().Be("Yazar zaten mevcut.");
    }
    [Fact]
    public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
    {
        CreateAuthorModel model = new CreateAuthorModel() { Name = "createAuthorTestName2", Surname = "createAuthorTestSurname2", Birthday = new DateTime(1969, 12, 31) };
        CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
        command.Model = model;
        FluentActions.Invoking(() => command.Handle()).Invoke();
        var author = _context.Authors.SingleOrDefault(x => x.Name == model.Name && x.Surname == model.Surname);
        author.Should().NotBeNull();
        author.Name.Should().Be(model.Name);
        author.Surname.Should().Be(model.Surname);
        author.Birthday.Date.Should().Be(model.Birthday.Date);


    }
}