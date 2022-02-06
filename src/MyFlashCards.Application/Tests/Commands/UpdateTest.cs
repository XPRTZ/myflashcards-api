using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFlashCards.Application.Exceptions;
using MyFlashCards.Application.Interfaces;
using MyFlashCards.Application.Tests.Validation;
using MyFlashCards.Domain.Models;

namespace MyFlashCards.Application.Tests.Commands;

public record UpdateTest(Guid Id, Test Test) : IRequest;

public class UpdateTestHandler : IRequestHandler<UpdateTest>
{
    private readonly IFlashCardsContext _context;

    public UpdateTestHandler(IFlashCardsContext context) => _context = context;

    public async Task<Unit> Handle(UpdateTest request, CancellationToken cancellationToken)
    {
        var (id, test) = request;

        var entity =
            _context.Tests
                .Include(x => x.Questions)
                .ThenInclude(x => x.Card)
                .SingleOrDefault(x => x.Id == id)
            ?? throw new NotFoundException($"Test with id {id} could not be found");

        entity.Prompt = test.Prompt;
        foreach (var (card, correct) in test.Questions)
        {
            var question = entity.Questions.Single(q => q.Card.Id == card.Id);
            question.Correct = correct;
        }

        await _context.SaveChanges(cancellationToken);

        return Unit.Value;
    }
}

public class UpdateTestValidator : AbstractValidator<UpdateTest>
{
    public UpdateTestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Test).SetValidator(new TestValidator());
    }
}
