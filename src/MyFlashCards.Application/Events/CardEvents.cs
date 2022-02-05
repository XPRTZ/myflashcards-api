using MyFlashCards.Application.Cards.Commands;

namespace MyFlashCards.Application.Events;

public class CardCreatedEvent : Event<AddCard>
{
    public CardCreatedEvent()
    {
        
    }
    
    public CardCreatedEvent(AddCard data)
    {
        Data = data;
        StreamId = data.Card.Id;
    }
}

public class CardUpdatedEvent : Event<UpdateCard>
{
    public CardUpdatedEvent()
    {
        
    }
    
    public CardUpdatedEvent(UpdateCard data)
    {
        Data = data;
        StreamId = data.Id;
    }
}

public class CardDeletedEvent : Event<DeleteCard>
{
    public CardDeletedEvent()
    {
        
    }
    
    public CardDeletedEvent(DeleteCard data)
    {
        Data = data;
        StreamId = data.Id;
    }
}
