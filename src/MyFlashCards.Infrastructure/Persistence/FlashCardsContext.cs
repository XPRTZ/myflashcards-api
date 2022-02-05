using Microsoft.EntityFrameworkCore;
using MyFlashCards.Application.Entities;
using MyFlashCards.Application.Events;
using MyFlashCards.Application.Interfaces;
using Newtonsoft.Json;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;

namespace MyFlashCards.Infrastructure.Persistence;

public class FlashCardsContext : DbContext, IFlashCardsContext
{
    public FlashCardsContext()
        : base(new DbContextOptionsBuilder<FlashCardsContext>().UseSqlServer("Server=.;Database=MyFlashCards;Trusted_Connection=True").Options) { }

    public FlashCardsContext(DbContextOptions options)
        : base(options) { }
    
    public DbSet<Card> Cards => Set<Card>();

    public DbSet<Test> Tests => Set<Test>();

    public DbSet<EventBase> Events => Set<EventBase>();
    public DbSet<CardCreatedEvent> CardCreatedEvents => Set<CardCreatedEvent>();
    public DbSet<CardDeletedEvent> CardDeletedEvents => Set<CardDeletedEvent>();
    public DbSet<CardUpdatedEvent> CardUpdatedEvents => Set<CardUpdatedEvent>();
    public DbSet<TestCreatedEvent> TestCreatedEvents => Set<TestCreatedEvent>();
    public DbSet<TestDeletedEvent> TestDeletedEvents => Set<TestDeletedEvent>();
    public DbSet<TestUpdatedEvent> TestUpdatedEvents => Set<TestUpdatedEvent>();


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

        modelBuilder.Entity<EventBase>()
            .Property(e => e.Timestamp)
            .HasDefaultValueSql("GETDATE()");
    }
}
