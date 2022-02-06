using MediatR;
using MyFlashCards.Application.Events;
using MyFlashCards.Application.Interfaces;
using MyFlashCards.Domain.Models;

namespace MyFlashCards.Application.Cards.Queries;

public record GetCards : IRequest<IEnumerable<Card>>;

public class GetCardsHandler : IRequestHandler<GetCards, IEnumerable<Card>>
{
    private readonly IFlashCardsContext _context;

    public GetCardsHandler(IFlashCardsContext context) => _context = context;

    public Task<IEnumerable<Card>> Handle(GetCards request, CancellationToken cancellationToken)
    {
        var cardEvents = _context.Events.ToList()
            .OfType<CardEvent>()
            .OrderBy(x => x.StreamPosition)
            .GroupBy(x => x.StreamId);

        var cards = cardEvents
            .Select(CardAggregate.Load)
            .Where(x => !x.IsDeleted)
            .Select(x => new Card(x.Id, x.Front, x.Back, x.Tags));

        return Task.FromResult(cards);
    }
}
