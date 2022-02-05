using Microsoft.EntityFrameworkCore;
using MyFlashCards.Application.Entities;
using MyFlashCards.Application.Events;

namespace MyFlashCards.Application.Interfaces;

public interface IFlashCardsContext
{
    DbSet<Card> Cards { get; }
    DbSet<Test> Tests { get; }
    DbSet<EventBase> Events { get; }
    
    Task<int> SaveChanges(CancellationToken cancellationToken = default);
}
