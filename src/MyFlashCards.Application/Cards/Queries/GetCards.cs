using MediatR;
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
        var cards = _context.Cards
            .Select(x => new Card(x.Id, x.Front, x.Back, x.Tags))
            .ToList();

        return Task.FromResult(cards as IEnumerable<Card>);
    }
}
