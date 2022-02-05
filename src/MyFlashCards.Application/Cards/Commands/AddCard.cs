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
    private IFlashCardsContext _context;

    public AddCardHandler(IFlashCardsContext context) => _context = context;

    public async Task<Unit> Handle(AddCard request, CancellationToken cancellationToken)
    {
        var card = request.Card;

        var entity = _context.Cards.SingleOrDefault(x => x.Id == card.Id);

        if (entity != null)
        {
            throw new AlreadyExistsException(nameof(Card), card.Id);
        }

        entity = new Entities.Card {Id = card.Id, Front = card.Front, Back = card.Back, Tags = card.Tags};

        _context.Cards.Add(entity);

        _context.Events.Add(new CardCreatedEvent(request));

        await _context.SaveChanges(cancellationToken);

        return Unit.Value;
    }
}

public class AddCardValidator : AbstractValidator<AddCard>
{
    public AddCardValidator() => RuleFor(x => x.Card).SetValidator(new CardValidator());
}
