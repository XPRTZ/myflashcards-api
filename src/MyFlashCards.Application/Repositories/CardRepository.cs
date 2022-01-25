using Microsoft.VisualBasic;
using MyFlashCards.Application.Entities;
using MyFlashCards.Application.Exceptions;
using MyFlashCards.Application.Interfaces;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using Card = MyFlashCards.Domain.Models.Card;

namespace MyFlashCards.Application.Repositories;

internal class CardRepository : ICardRepository
{
    private readonly IFlashCardsContext _context;

    public CardRepository(IFlashCardsContext context) => _context = context;
    
    public Task<IEnumerable<Card>> Get(CancellationToken cancellationToken = default)
    {
        var cards = _context.Cards
            .Select(x => new Card(x.Id, x.Front, x.Back, x.Tags))
            .ToList();

        return Task.FromResult(cards as IEnumerable<Card>);
    }

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
