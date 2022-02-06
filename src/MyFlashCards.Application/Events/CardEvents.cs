using MyFlashCards.Domain.Models;

namespace MyFlashCards.Application.Events;

public class CardEvent : Event<Card>
{
}

public class CardCreatedEvent : CardEvent
{
    public CardCreatedEvent()
    {
        
    }
    
    public CardCreatedEvent(Card data)
    {
        Data = data;
        StreamId = data.Id;
    }
}

public class CardUpdatedEvent : CardEvent
{
    public CardUpdatedEvent()
    {
        
    }
    
    public CardUpdatedEvent(Card data)
    {
        Data = data;
        StreamId = data.Id;
    }
}

public class CardDeletedEvent : CardEvent
{
    public CardDeletedEvent()
    {
        
    }
    
    public CardDeletedEvent(Card data)
    {
        Data = data;
        StreamId = data.Id;
    }
}
