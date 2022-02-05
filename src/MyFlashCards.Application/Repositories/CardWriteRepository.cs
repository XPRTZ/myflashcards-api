using MyFlashCards.Application.Exceptions;
using MyFlashCards.Application.Interfaces;
using Card = MyFlashCards.Domain.Models.Card;

namespace MyFlashCards.Application.Repositories;

internal class CardWriteRepository : ICardWriteRepository
{
    private readonly IFlashCardsContext _context;

    public CardWriteRepository(IFlashCardsContext context) => _context = context;

    public async Task Add(Card card, CancellationToken cancellationToken = default)
    {
        var entity = _context.Cards.SingleOrDefault(x => x.Id == card.Id);

        if (entity != null)
        {
            throw new AlreadyExistsException(nameof(Card), card.Id);
        }

        entity = new Entities.Card {Id = card.Id, Front = card.Front, Back = card.Back, Tags = card.Tags};

        _context.Cards.Add(entity);

        await _context.SaveChanges(cancellationToken);
    }

    public async Task Update(Guid id, Card card, CancellationToken cancellationToken = default)
    {
        var entity = 
            _context.Cards.SingleOrDefault(x => x.Id == id)
            ?? throw new NotFoundException($"Card with id {id} could not be found");

        entity.Front = card.Front;
        entity.Back = card.Back;
        entity.Tags = card.Tags;

        await _context.SaveChanges(cancellationToken);
    }

    public async Task Delete(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = 
            _context.Cards.SingleOrDefault(x => x.Id == id)
            ?? throw new NotFoundException($"Card with id {id} could not be found");

        _context.Cards.Remove(entity);

        await _context.SaveChanges(cancellationToken);
    }
}
