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
    private readonly IFlashCardsContext _context;

    public UpdateCardHandler(IFlashCardsContext context) => _context = context;

    public async Task<Unit> Handle(UpdateCard request, CancellationToken cancellationToken)
    {
        var cardExits = _context.Events.ToList()
            .OfType<CardEvent>()
            .Any(x => x.StreamId == request.Id);

        if (!cardExits)
        {
            throw new NotFoundException($"Card with id {request.Id} could not be found");
        }

        _context.Events.Add(new CardUpdatedEvent(request.Card with {Id = request.Id}));
        
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
