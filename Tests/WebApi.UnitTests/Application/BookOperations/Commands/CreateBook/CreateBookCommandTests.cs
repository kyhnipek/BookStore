using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Application.BookOperations.Commands.CreateBook;

public class CreateBookCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public CreateBookCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact] //Bir koşulda çalışan test için kullanılır.
    public void WhenAlreadyExistBookIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        //arrange (hazırlık)
        var book = new Book() { Title = "Test", GenreId = 2, AuthorId = 2, PageCount = 200, PublishDate = new DateTime(2000, 01, 10) };
        _context.Books.Add(book);
        _context.SaveChanges();
        CreateBookCommand command = new CreateBookCommand(_context, _mapper);
        command.Model = new CreateBookModel() { Title = book.Title };

        //act (çalıştırma) & assert (doğrulama)
        FluentActions
        .Invoking(() => command.Handle())
        .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap zaten mevcut.");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
    {
        //arrange
        CreateBookCommand command = new CreateBookCommand(_context, _mapper);
        CreateBookModel model = new CreateBookModel() { Title = "Hobbit", GenreId = 2, AuthorId = 2, PageCount = 200, PublishDate = new DateTime(2000, 01, 10) };
        command.Model = model;

        //act
        FluentActions.Invoking(() => command.Handle()).Invoke(); // invoking'den sonra should kullanılmadıysa çalışmaz Invoke() methoduyla çalıştırılması gerekir.

        //assert
        var book = _context.Books.SingleOrDefault(book => book.Title == model.Title);
        book.Should().NotBeNull();
        book.PageCount.Should().Be(model.PageCount);
        book.AuthorId.Should().Be(model.AuthorId);
        book.GenreId.Should().Be(model.GenreId);
        book.PublishDate.Should().Be(model.PublishDate);
    }
}