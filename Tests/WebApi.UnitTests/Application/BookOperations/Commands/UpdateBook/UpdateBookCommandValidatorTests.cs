using FluentAssertions;
using WebApi.Application.BookOperations.Commands.UpdateBook;

namespace Application.BookOperations.Commands.UpdateBook;

public class UpdateBookCommandValidatorTests
{
    [Theory]
    [InlineData(0, "Test", 1, 1)]
    [InlineData(1, "Test", 0, 1)]
    [InlineData(1, "Test", 1, 0)]
    [InlineData(1, "Tes", 1, 1)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int id, string title, int genreId, int authorId)
    {
        UpdateBookCommand command = new UpdateBookCommand(null, null);
        command.Id = id;
        command.Model = new UpdateBookModel() { Title = title, AuthorId = authorId, GenreId = genreId };
        UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
        var result = validator.Validate(command);
        result.Errors.Count.Should().BeGreaterThan(0);
    }
    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
    {
        UpdateBookCommand command = new UpdateBookCommand(null, null);
        command.Id = 1;
        command.Model = new UpdateBookModel() { Title = "Test", GenreId = 1, AuthorId = 1 };
        UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
        var result = validator.Validate(command);
        result.Errors.Count.Should().Be(0);
    }
}