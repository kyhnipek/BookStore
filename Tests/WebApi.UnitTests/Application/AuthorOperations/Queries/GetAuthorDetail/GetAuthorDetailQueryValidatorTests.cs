using FluentAssertions;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;

namespace Application.AuthorOperations.Queries.GetAuthorDetail;

public class GetAuthorDetailQueryValidatorTests
{
    [Fact]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
    {
        GetAuthorDetailQuery command = new GetAuthorDetailQuery(null, null);
        command.AuthorId = 0;
        GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
        var result = validator.Validate(command);
        result.Errors.Count.Should().BeGreaterThan(0);
    }
    [Fact]
    public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnErrors()
    {
        GetAuthorDetailQuery command = new GetAuthorDetailQuery(null, null);
        command.AuthorId = 1;
        GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
        var result = validator.Validate(command);
        result.Errors.Count.Should().Be(0);
    }
}
