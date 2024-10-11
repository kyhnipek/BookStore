using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.CreateGenre;

namespace Application.GenreOperations.Commands.CreateGenre;

public class CreateGenreCommandValidatorTests
{
    [Fact]
    public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors()
    {
        CreateGenreCommand command = new CreateGenreCommand(null);
        command.Model = new CreateGenreModel() { Name = "" };
        CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
        var result = validator.Validate(command);
        result.Errors.Count.Should().Be(1);
    }
    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors()
    {
        CreateGenreCommand command = new CreateGenreCommand(null);
        command.Model = new CreateGenreModel() { Name = "test" };
        CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
        var result = validator.Validate(command);
        result.Errors.Count.Should().Be(0);
    }
}