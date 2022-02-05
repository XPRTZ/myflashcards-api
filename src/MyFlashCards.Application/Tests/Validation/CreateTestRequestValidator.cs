using FluentValidation;
using MyFlashCards.Domain.Requests;

namespace MyFlashCards.Application.Tests.Validation;

public class CreateTestRequestValidator : AbstractValidator<CreateTestRequest>
{
    public CreateTestRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Prompt).NotEmpty().IsInEnum();
        RuleFor(x => x.NumberOfQuestions).NotEmpty();
    }
}
