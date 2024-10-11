using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Application.BookOperations.Commands.CreateBook;

public class CreateBookCommandValidatorTests
{
    [Theory] //Birden fazla veri için ,birden fazla kez çalışması gerekiyorsa Theory kullanılır.
    [InlineData("Lord Of The Rings", 0, 0, 0)]
    [InlineData("Lord Of The Rings", 0, 0, 1)]
    [InlineData("Lord Of The Rings", 0, 1, 0)]
    [InlineData("Lord Of The Rings", 0, 1, 1)]
    [InlineData("Lord Of The Rings", 1, 0, 0)]
    [InlineData("Lord Of The Rings", 1, 0, 1)]
    [InlineData("Lord Of The Rings", 1, 1, 0)]
    [InlineData("", 1, 1, 1)]
    [InlineData(" ", 1, 1, 1)]
    [InlineData("Lor", 1, 1, 1)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int genreId, int pageCount, int authorId)
    {
        //arrange
        CreateBookCommand command = new CreateBookCommand(null, null);
        command.Model = new CreateBookModel()
        {
            Title = title,
            GenreId = genreId,
            PageCount = pageCount,
            AuthorId = authorId,
            PublishDate = DateTime.Now.Date.AddYears(-1)
        };
        //act
        CreateBookCommandValidator validator = new CreateBookCommandValidator();
        var result = validator.Validate(command);
        //assertion
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenDatetimeEqualNowIsGiven_Validator_ShoulBeReturnError()
    {
        CreateBookCommand command = new CreateBookCommand(null, null);
        command.Model = new CreateBookModel()
        {
            Title = "Lord Of The Rings",
            GenreId = 2,
            PageCount = 100,
            AuthorId = 1,
            PublishDate = DateTime.Now.Date
        };
        CreateBookCommandValidator validator = new CreateBookCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);

    }
    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
    {
        CreateBookCommand command = new CreateBookCommand(null, null);
        command.Model = new CreateBookModel()
        {
            Title = "Lord Of The Rings",
            GenreId = 2,
            PageCount = 100,
            AuthorId = 1,
            PublishDate = DateTime.Now.AddYears(-1)
        };

        CreateBookCommandValidator validator = new CreateBookCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);

    }

}