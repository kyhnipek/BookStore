using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;

namespace Application.GenreOperations.Commands.UpdateGenre;
public class UpdateGenreCommandValidatorTests
{
    [Fact]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
    {
        UpdateGenreCommand command = new UpdateGenreCommand(null, null);
        command.GenreId = 0;
        command.Model = new UpdateGenreModel() { Name = "test", IsActive = true };
        UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
        var result = validator.Validate(command);
        result.Errors.Count.Should().BeGreaterThan(0);
    }
    [Fact]
    public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnErrors()
    {
        UpdateGenreCommand command = new UpdateGenreCommand(null, null);
        command.GenreId = 1;
        command.Model = new UpdateGenreModel() { Name = "test", IsActive = true };
        UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
        var result = validator.Validate(command);
        result.Errors.Count.Should().Be(0);
    }
}