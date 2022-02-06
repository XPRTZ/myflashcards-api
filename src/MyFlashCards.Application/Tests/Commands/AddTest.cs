using FluentValidation;
using MediatR;
using MyFlashCards.Application.Events;
using MyFlashCards.Application.Exceptions;
using MyFlashCards.Application.Extensions;
using MyFlashCards.Application.Interfaces;
using MyFlashCards.Application.Tests.Validation;
using MyFlashCards.Domain.Models;
using MyFlashCards.Domain.Requests;
using Card = MyFlashCards.Domain.Models.Card;
using Question = MyFlashCards.Domain.Models.Question;

namespace MyFlashCards.Application.Tests.Commands;

public record AddTest(CreateTestRequest Request) : IRequest;

public class AddTestHandler : IRequestHandler<AddTest>
{
    private readonly IFlashCardsContext _context;

    public AddTestHandler(IFlashCardsContext context) => _context = context;

    public async Task<Unit> Handle(AddTest request, CancellationToken cancellationToken)
    {
        var (id, prompt, numberOfQuestions, tags) = request.Request;

        var testExits = _context.Events.ToList()
            .OfType<TestEvent>()
            .Any(x => x.StreamId == id);

        if (testExits)
        {
            throw new AlreadyExistsException(nameof(Test), id);
        }

        tags = tags.ToList();

        var cardEvents = _context.Events.ToList()
            .OfType<CardEvent>()
            .OrderBy(x => x.StreamPosition)
            .GroupBy(x => x.StreamId)
            .ToList();

        var cards = cardEvents
            .Select(CardAggregate.Load)
            .Where(x => !x.IsDeleted)
            .Select(x => new Card(x.Id, x.Front, x.Back, x.Tags));

        cards = cards
            .Where(x => !tags.Any() || x.Tags.Any(t => tags.Contains(t)))
            .Shuffle()
            .Take(numberOfQuestions);

        var questions = cards.Select(x => new Question(x, false));

        var test = new Test(id, prompt, questions);

        _context.Events.Add(new TestCreatedEvent(test));

        await _context.SaveChanges(cancellationToken);

        return Unit.Value;
    }
}

public class AddTestValidator : AbstractValidator<AddTest>
{
    public AddTestValidator() => RuleFor(x => x.Request).SetValidator(new CreateTestRequestValidator());
}
