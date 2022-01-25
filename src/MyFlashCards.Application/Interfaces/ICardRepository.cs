using MyFlashCards.Domain.Models;

namespace MyFlashCards.Application.Interfaces;

public interface ICardRepository
{
    Task<IEnumerable<Card>> Get(CancellationToken cancellationToken = default);
    Task Add(Card card, CancellationToken cancellationToken = default);
    Task Update(Guid id, Card card, CancellationToken cancellationToken = default);
    Task Delete(Guid id, CancellationToken cancellationToken = default);
}
