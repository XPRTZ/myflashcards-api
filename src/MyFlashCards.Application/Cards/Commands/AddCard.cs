using FluentValidation;
using MediatR;
using MyFlashCards.Application.Cards.Validation;
using MyFlashCards.Application.Events;
using MyFlashCards.Application.Exceptions;
using MyFlashCards.Application.Interfaces;
using MyFlashCards.Domain.Models;

namespace MyFlashCards.Application.Cards.Commands;

public record AddCard(Card Card) : IRequest;

public class AddCardHandler : IRequestHandler<AddCard>
{
    private readonly IFlashCardsContext _context;

    public AddCardHandler(IFlashCardsContext context) => _context = context;

    public async Task<Unit> Handle(AddCard request, CancellationToken cancellationToken)
    {
        var cardExits = _context.Events.ToList()
            .OfType<CardEvent>()
            .Any(x => x.StreamId == request.Card.Id);
        
        if (cardExits)
        {
            throw new AlreadyExistsException(nameof(Card), request.Card.Id);
        }

        _context.Events.Add(new CardCreatedEvent(request.Card));

        await _context.SaveChanges(cancellationToken);

        return Unit.Value;
    }
}

public class AddCardValidator : AbstractValidator<AddCard>
{
    public AddCardValidator() => RuleFor(x => x.Card).SetValidator(new CardValidator());
}
