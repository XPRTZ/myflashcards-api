using Microsoft.EntityFrameworkCore;
using MyFlashCards.Application.Entities;

namespace MyFlashCards.Application.Interfaces;

public interface IFlashCardsContext
{
    DbSet<Card> Cards { get; }
    DbSet<Test> Tests { get; }

    Task<int> SaveChanges(CancellationToken cancellationToken = default);
}
