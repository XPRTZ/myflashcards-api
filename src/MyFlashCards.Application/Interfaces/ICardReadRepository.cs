using MyFlashCards.Domain.Models;

namespace MyFlashCards.Application.Interfaces;

public interface ICardReadRepository
{
    IEnumerable<Card> Get();
}
