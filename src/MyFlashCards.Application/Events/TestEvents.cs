using MyFlashCards.Application.Tests.Commands;

namespace MyFlashCards.Application.Events;

public class TestCreatedEvent : Event<AddTest>
{
    public TestCreatedEvent()
    {
        
    }
    
    public TestCreatedEvent(AddTest data)
    {
        Data = data;
        StreamId = data.Request.Id;
    }
}

public class TestUpdatedEvent : Event<UpdateTest>
{
    public TestUpdatedEvent()
    {
        
    }
    
    public TestUpdatedEvent(UpdateTest data)
    {
        Data = data;
        StreamId = data.Id;
    }
}

public class TestDeletedEvent : Event<DeleteTest>
{
    public TestDeletedEvent()
    {
        
    }
    
    public TestDeletedEvent(DeleteTest data)
    {
        Data = data;
        StreamId = data.Id;
    }
}
