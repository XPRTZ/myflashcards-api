using FluentValidation;
using MyFlashCards.Application.Cards.Validation;
using MyFlashCards.Domain.Models;

namespace MyFlashCards.Application.Tests.Validation;

public class QuestionValidator:AbstractValidator<Question>
{
    public QuestionValidator() => RuleFor(x => x.Card).SetValidator(new CardValidator());
}
