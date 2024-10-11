using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;

namespace Application.AuthorOperations.Commands.DeleteAuthor;

public class DeleteAuthorCommandValidatorTests
{
    [Fact]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
    {
        DeleteAuthorCommand command = new DeleteAuthorCommand(null);
        command.AuthorId = 0;
        DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
        var result = validator.Validate(command);
        result.Errors.Count.Should().BeGreaterThan(0);
    }
    [Fact]
    public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnErrors()
    {
        DeleteAuthorCommand command = new DeleteAuthorCommand(null);
        command.AuthorId = 1;
        DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
        var result = validator.Validate(command);
        result.Errors.Count.Should().Be(0);
    }
}