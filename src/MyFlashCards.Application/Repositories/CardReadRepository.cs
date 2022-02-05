using MyFlashCards.Application.Interfaces;
using Card = MyFlashCards.Domain.Models.Card;

namespace MyFlashCards.Application.Repositories;

internal class CardReadRepository : ICardReadRepository
{
    private readonly IFlashCardsContext _context;

    public CardReadRepository(IFlashCardsContext context) => _context = context;
    
    public IEnumerable<Card> Get()
    {
        var cards = _context.Cards
            .Select(x => new Card(x.Id, x.Front, x.Back, x.Tags))
            .ToList();

        return cards;
    }
}
