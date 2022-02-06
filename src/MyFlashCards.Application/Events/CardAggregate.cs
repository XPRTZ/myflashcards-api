namespace MyFlashCards.Application.Events;

public class CardAggregate
{
    public Guid Id { get; private set; }
    
    public string Front { get; private set; } = default!;
    
    public string Back { get; private set; } = default!;

    public IEnumerable<string> Tags { get; private set; } = Enumerable.Empty<string>();
    
    public bool IsDeleted { get; private set; }

    private void Apply(CardEvent cardEvent)
    {
        switch (cardEvent)
        {
            case CardCreatedEvent createdEvent:
                ApplyCreatedEvent(createdEvent);
                break;
            case CardUpdatedEvent updatedEvent:
                ApplyUpdatedEvent(updatedEvent);
                break;
            case CardDeletedEvent deletedEvent:
                ApplyDeletedEvent(deletedEvent);
                break;
        }
    }

    private void ApplyCreatedEvent(CardCreatedEvent createdEvent)
    {
        if (Id != Guid.Empty)
        {
            throw new InvalidOperationException("Applying card created event on an already created aggregate");
        }

        var data = createdEvent.Data;

        Id = data.Id;
        Front = data.Front;
        Back = data.Back;
        Tags = data.Tags;
    }

    private void ApplyUpdatedEvent(CardUpdatedEvent updatedEvent)
    {
        if (updatedEvent.StreamId != Id)
        {
            throw new InvalidOperationException("Applying card updated event on the wrong aggregate");
        }

        var data = updatedEvent.Data;

        Front = data.Front;
        Back = data.Back;
        Tags = data.Tags;
    }

    private void ApplyDeletedEvent(CardDeletedEvent deletedEvent)
    {
        if (deletedEvent.StreamId != Id)
        {
            throw new InvalidOperationException("Applying card deleted event on the wrong aggregate");
        }

        IsDeleted = true;
    }

    public static CardAggregate Load(IEnumerable<CardEvent> events)
    {
        var aggregate = new CardAggregate();
        foreach (var cardEvent in events.OrderBy(x => x.StreamPosition))
        {
            aggregate.Apply(cardEvent);
        }

        return aggregate;
    }
}
