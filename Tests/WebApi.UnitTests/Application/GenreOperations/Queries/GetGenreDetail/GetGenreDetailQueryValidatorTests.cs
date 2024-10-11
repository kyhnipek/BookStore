using FluentAssertions;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;

namespace Application.GenreOperations.Queries.GetGenreDetail;
public class GetGenreDetailQueryValidatorTests
{
    [Fact]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
    {
        GetGenreDetailQuery command = new GetGenreDetailQuery(null, null);
        command.Id = 0;
        GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
        var result = validator.Validate(command);
        result.Errors.Count.Should().BeGreaterThan(0);
    }
    [Fact]
    public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnErrors()
    {
        GetGenreDetailQuery command = new GetGenreDetailQuery(null, null);
        command.Id = 1;
        GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
        var result = validator.Validate(command);
        result.Errors.Count.Should().Be(0);
    }
}