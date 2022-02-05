using FluentValidation;
using MyFlashCards.Domain.Models;
using System.Data;

namespace MyFlashCards.Application.Cards.Validation;

public class CardValidator : AbstractValidator<Card>
{
    public CardValidator()
    {
        RuleFor(x => x.Front).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Back).NotEmpty().MaximumLength(50);
    }
}
