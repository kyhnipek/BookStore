using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor;


public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
{
    public UpdateAuthorCommandValidator()
    {
        RuleFor(c => c.AuthorId).GreaterThan(0);
        RuleFor(c => c.Model.Name).NotEmpty().MinimumLength(2).When(c => c.Model.Name.Trim() != string.Empty); ;
        RuleFor(c => c.Model.Surname).NotEmpty().MinimumLength(2).When(c => c.Model.Surname.Trim() != string.Empty); ;
        // RuleFor(c => c.Model.Birthday).NotEmpty();
    }
}