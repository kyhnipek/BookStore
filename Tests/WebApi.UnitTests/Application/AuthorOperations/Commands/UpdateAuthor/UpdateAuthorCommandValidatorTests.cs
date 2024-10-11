using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;

namespace Application.AuthorOperations.Commands.UpdateAuthor;

public class UpdateAuthorCommandValidatorTests
{
    [Fact]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
    {
        UpdateAuthorCommand command = new UpdateAuthorCommand(null, null);
        command.AuthorId = 0;
        command.Model = new UpdateAuthorModel() { Name = "test1", Surname = "test1" };
        UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
        var result = validator.Validate(command);
        result.Errors.Count.Should().BeGreaterThan(0);
    }
    [Fact]
    public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnErrors()
    {
        UpdateAuthorCommand command = new UpdateAuthorCommand(null, null);
        command.AuthorId = 1;
        command.Model = new UpdateAuthorModel() { Name = "test2", Surname = "test2" };
        UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
        var result = validator.Validate(command);
        result.Errors.Count.Should().Be(0);
    }
}