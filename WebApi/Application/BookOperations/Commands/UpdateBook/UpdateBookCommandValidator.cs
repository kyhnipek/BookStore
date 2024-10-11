using FluentValidation;

namespace WebApi.Application.BookOperations.Commands.UpdateBook;

public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookCommandValidator()
    {
        RuleFor(command => command.Id).GreaterThan(0);
        RuleFor(command => command.Model.GenreId).GreaterThan(0);
        RuleFor(command => command.Model.Title).MinimumLength(4).When(command => command.Model.Title.Trim() != string.Empty);
        RuleFor(command => command.Model.AuthorId).GreaterThan(0);
    }
}