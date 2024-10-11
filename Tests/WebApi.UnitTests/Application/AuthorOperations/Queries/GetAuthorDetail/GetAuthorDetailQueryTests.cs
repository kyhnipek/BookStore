using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.DBOperations;

namespace Application.AuthorOperations.Queries.GetAuthorDetail;

public class GetAuthorDetailQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetAuthorDetailQueryTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenAuthorIsNotExist_InvalidOperationException_ShouldBeReturn()
    {
        GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
        query.AuthorId = 500;
        FluentActions.Invoking(() => query.Handle())
                        .Should().Throw<InvalidOperationException>()
                        .And.Message.Should().Be("Yazar bulunamadı.");
    }
    [Fact]
    public void WhenValidInputIsGiven_Author_ShouldBeReturned()
    {
        var author = _context.Authors.SingleOrDefault(x => x.Id == 1);
        GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
        query.AuthorId = author.Id;
        AuthorDetailViewModel vm = query.Handle();
        vm.Should().NotBeNull();
        vm.Name.Should().Be(author.Name);
        vm.Surname.Should().Be(author.Surname);
        vm.Birthday.Should().Be(author.Birthday.Date.ToString("dd/MM/yyyy"));
    }
}