using FluentValidation;
using MediatR;
using MyFlashCards.Application.Cards.Validation;
using MyFlashCards.Application.Events;
using MyFlashCards.Application.Exceptions;
using MyFlashCards.Application.Interfaces;
using MyFlashCards.Domain.Models;

namespace MyFlashCards.Application.Cards.Commands;

public record UpdateCard(Guid Id, Card Card) : IRequest;

public class UpdateCardHandler : IRequestHandler<UpdateCard>
{
    private IFlashCardsContext _context;

    public UpdateCardHandler(IFlashCardsContext context) => _context = context;

    public async Task<Unit> Handle(UpdateCard request, CancellationToken cancellationToken)
    {
        var (id, card) = request;

        var entity =
            _context.Cards.SingleOrDefault(x => x.Id == id)
            ?? throw new NotFoundException($"Card with id {id} could not be found");

        entity.Front = card.Front;
        entity.Back = card.Back;
        entity.Tags = card.Tags;

        _context.Events.Add(new CardUpdatedEvent(request));
        
        await _context.SaveChanges(cancellationToken);

        return Unit.Value;
    }
}

public class UpdateCardValidator : AbstractValidator<UpdateCard>
{
    public UpdateCardValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Card).SetValidator(new CardValidator());
    }
}
