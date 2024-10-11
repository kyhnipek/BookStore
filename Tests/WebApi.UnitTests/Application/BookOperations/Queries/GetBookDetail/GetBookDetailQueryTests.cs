using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TestSetup;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.DBOperations;

namespace Application.BookOperations.Queries.GetBookDetail;

public class GetBookDetailQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetBookDetailQueryTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    public int BookId { get; set; }



    [Fact]
    public void WhenNotExistBookIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
        query.Id = 500;
        FluentActions.Invoking(() => query.Handle())
                      .Should().Throw<InvalidOperationException>()
                      .And.Message.Should().Be("Kitap bulunamadı.");
    }
    [Fact]
    public void WhenValidInputIsGiven_Book_ShouldBeReturned()
    {
        BookId = 3;
        var book = _context.Books.Include(x => x.Author).Include(x => x.Genre).Where(x => x.Id == BookId).SingleOrDefault();
        GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
        query.Id = book.Id;
        GetBookDetailViewModel vm = query.Handle();
        vm.Title.Should().Be(book.Title);
        vm.PageCount.Should().Be(book.PageCount);
        vm.PublishDate.Should().Be((book.PublishDate.Date).ToString("dd/MM/yyy"));
        vm.AuthorName.Should().Be(book.Author.Name + " " + book.Author.Surname);
        vm.Genre.Should().Be(book.Genre.Name);
    }


}