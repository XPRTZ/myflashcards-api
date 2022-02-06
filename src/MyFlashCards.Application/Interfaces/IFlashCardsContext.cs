using Microsoft.EntityFrameworkCore;
using MyFlashCards.Application.Events;

namespace MyFlashCards.Application.Interfaces;

public interface IFlashCardsContext
{
    DbSet<EventBase> Events { get; }
    
    Task<int> SaveChanges(CancellationToken cancellationToken = default);
}
