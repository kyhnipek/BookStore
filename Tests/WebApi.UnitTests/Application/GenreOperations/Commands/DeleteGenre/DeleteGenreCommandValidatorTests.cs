using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;

namespace Application.GenreOperations.Commands.DeleteGenre;
public class DeleteGenreCommandValidatorTests
{
    [Fact]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
    {
        DeleteGenreCommand command = new DeleteGenreCommand(null);
        command.GenreId = 0;
        DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
        var result = validator.Validate(command);
        result.Errors.Count.Should().BeGreaterThan(0);
    }
    [Fact]
    public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnErrors()
    {
        DeleteGenreCommand command = new DeleteGenreCommand(null);
        command.GenreId = 1;
        DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
        var result = validator.Validate(command);
        result.Errors.Count.Should().Be(0);
    }
}