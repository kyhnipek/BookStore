using FluentAssertions;
using WebApi.Application.BookOperations.Queries.GetBookDetail;

namespace Application.BookOperations.Queries.GetBookDetail;

public class GetBookDetailQueryValidatorTests
{
    [Fact]
    public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnError()
    {
        GetBookDetailQuery query = new GetBookDetailQuery(null, null);
        query.Id = 0;
        GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
        var result = validator.Validate(query);
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnError()
    {
        GetBookDetailQuery query = new GetBookDetailQuery(null, null);
        query.Id = 1;
        GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
        var result = validator.Validate(query);
        result.Errors.Count.Should().Be(0);
    }
}