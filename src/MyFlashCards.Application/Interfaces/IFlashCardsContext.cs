using Microsoft.EntityFrameworkCore;
using MyFlashCards.Application.Entities;

namespace MyFlashCards.Application.Interfaces;

public interface IFlashCardsContext
{
    DbSet<Card> Cards { get; }

    Task<int> SaveChanges(CancellationToken cancellationToken = default);
}
