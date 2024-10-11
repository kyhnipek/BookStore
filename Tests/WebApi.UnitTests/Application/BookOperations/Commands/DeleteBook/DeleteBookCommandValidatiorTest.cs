using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.DeleteBook;

namespace Application.BookOperations.Commands.DeleteBook;

public class DeleteBookCommandValidatorTests
{
    [Fact]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
    {
        DeleteBookCommand command = new DeleteBookCommand(null);
        command.Id = 0;
        DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
        var result = validator.Validate(command);
        result.Errors.Count.Should().BeGreaterThan(0);
    }
    [Fact]
    public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnErrors()
    {
        DeleteBookCommand command = new DeleteBookCommand(null);
        command.Id = 1;
        DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
        var result = validator.Validate(command);
        result.Errors.Count.Should().Be(0);
    }
}