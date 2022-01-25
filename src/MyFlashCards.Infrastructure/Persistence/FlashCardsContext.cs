using Microsoft.EntityFrameworkCore;
using MyFlashCards.Application.Entities;
using MyFlashCards.Application.Interfaces;

namespace MyFlashCards.Infrastructure.Persistence;

public class FlashCardsContext : DbContext, IFlashCardsContext
{
    public FlashCardsContext()
        : base(new DbContextOptionsBuilder<FlashCardsContext>().UseSqlServer("Server=.;Database=MyFlashCards;Trusted_Connection=True").Options) { }

    public FlashCardsContext(DbContextOptions options)
        : base(options) { }
    
    public DbSet<Card> Cards => Set<Card>();
    public DbSet<Tag> Tags => Set<Tag>();

    public Task<int> SaveChanges(CancellationToken cancellationToken = default) =>
        base.SaveChangesAsync(cancellationToken);
}
