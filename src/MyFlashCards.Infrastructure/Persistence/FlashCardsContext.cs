using Microsoft.EntityFrameworkCore;
using MyFlashCards.Application.Entities;
using MyFlashCards.Application.Interfaces;
using System.Globalization;

namespace MyFlashCards.Infrastructure.Persistence;

public class FlashCardsContext : DbContext, IFlashCardsContext
{
    public FlashCardsContext()
        : base(new DbContextOptionsBuilder<FlashCardsContext>().UseSqlServer("Server=.;Database=MyFlashCards;Trusted_Connection=True").Options) { }

    public FlashCardsContext(DbContextOptions options)
        : base(options) { }
    
    public DbSet<Card> Cards => Set<Card>();

    public DbSet<Test> Tests => Set<Test>();

    public Task<int> SaveChanges(CancellationToken cancellationToken = default) =>
        base.SaveChangesAsync(cancellationToken);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Card>()
            .Property(e => e.Tags)
            .HasConversion(
                v => string.Join(',', v.Select(x => x.ToUpper(CultureInfo.InvariantCulture))),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

        modelBuilder.Entity<Test>()
            .Property(e => e.Prompt)
            .HasConversion<string>();
    }
}
