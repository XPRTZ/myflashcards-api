using FluentValidation;
using MediatR;
using MyFlashCards.Application.Events;
using MyFlashCards.Application.Extensions;
using MyFlashCards.Application.Interfaces;
using MyFlashCards.Application.Tests.Validation;
using MyFlashCards.Domain.Requests;

namespace MyFlashCards.Application.Tests.Commands;

public record AddTest(CreateTestRequest Request) : IRequest;

public class AddTestHandler : IRequestHandler<AddTest>
{
    private IFlashCardsContext _context;

    public AddTestHandler(IFlashCardsContext context) => _context = context;

    public async Task<Unit> Handle(AddTest request, CancellationToken cancellationToken)
    {
        var (id, prompt, numberOfQuestions, tags) = request.Request;

        tags = tags.ToList();

        var cards = _context.Cards.ToList()
            .Where(x => !tags.Any() || x.Tags.Any(t => tags.Contains(t)))
            .Shuffle()
            .Take(numberOfQuestions);

        var questions = cards.Select(x => new Entities.Question {Id = Guid.NewGuid(), Card = x});

        var test = new Entities.Test {Id = id, Prompt = prompt, Questions = questions.ToList()};

        _context.Tests.Add(test);

        _context.Events.Add(new TestCreatedEvent(request));

        await _context.SaveChanges(cancellationToken);

        return Unit.Value;
    }
}

public class AddTestValidator : AbstractValidator<AddTest>
{
    public AddTestValidator() => RuleFor(x => x.Request).SetValidator(new CreateTestRequestValidator());
}
