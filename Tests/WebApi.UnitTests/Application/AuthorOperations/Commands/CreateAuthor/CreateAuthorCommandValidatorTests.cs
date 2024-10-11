using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;

namespace Application.AuthorOperations.Commands.CreateAuthor;

public class CreateAuthorCommandValidatorTests
{
    [Theory]
    [InlineData("t", "t")]
    [InlineData("t", "te")]
    [InlineData("te", "t")]
    [InlineData("t", "")]
    [InlineData("", "t")]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name, string surname)
    {
        CreateAuthorCommand command = new CreateAuthorCommand(null, null);
        command.Model = new CreateAuthorModel()
        {
            Name = name,
            Surname = surname,
            Birthday = DateTime.Now.Date.AddYears(-1)
        };
        CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
        var result = validator.Validate(command);
        result.Errors.Count.Should().BeGreaterThan(0);
    }
    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
    {
        CreateAuthorCommand command = new CreateAuthorCommand(null, null);
        command.Model = new CreateAuthorModel()
        {
            Name = "test3",
            Surname = "test3",
            Birthday = DateTime.Now.Date.AddYears(-1)
        };

        CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);

    }
}