using FluentValidation;
using MediatR;
using MyFlashCards.Application.Events;
using MyFlashCards.Application.Exceptions;
using MyFlashCards.Application.Interfaces;
using MyFlashCards.Domain.Models;

namespace MyFlashCards.Application.Cards.Commands;

public record DeleteCard(Guid Id) : IRequest;

public class DeleteCardHandler : IRequestHandler<DeleteCard>
{
    private readonly IFlashCardsContext _context;

    public DeleteCardHandler(IFlashCardsContext context) => _context = context;

    public async Task<Unit> Handle(DeleteCard request, CancellationToken cancellationToken)
    {
        var cardExits = _context.Events.ToList()
            .OfType<CardEvent>()
            .Any(x => x.StreamId == request.Id);

        if (!cardExits)
        {
            throw new NotFoundException($"Card with id {request.Id} could not be found");
        }

        _context.Events.Add(new CardDeletedEvent(new Card(request.Id, string.Empty, string.Empty, Enumerable.Empty<string>())));

        await _context.SaveChanges(cancellationToken);

        return Unit.Value;
    }
}

public class DeleteCardValidator : AbstractValidator<DeleteCard>
{
    public DeleteCardValidator() => RuleFor(x => x.Id).NotEmpty();
}
