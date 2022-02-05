using FluentValidation;
using MyFlashCards.Domain.Models;

namespace MyFlashCards.Application.Tests.Validation;

public class TestValidator :AbstractValidator<Test>
{
    public TestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Prompt).NotEmpty().IsInEnum();
        RuleForEach(x => x.Questions).SetValidator(new QuestionValidator());
    }
}
